using System;
using System.Collections.Generic;

namespace KratosCore
{
    public class SmallDisplacementElement : Element
    {
        private readonly double _youngModulus;
        private readonly double _poissonRatio;
        private readonly double _thickness;

        public SmallDisplacementElement(int id, IGeometry geometry, IReadOnlyList<INode> nodes, 
            double youngModulus = 200e9, double poissonRatio = 0.3, double thickness = 1.0)
            : base(id, geometry, nodes)
        {
            _youngModulus = youngModulus;
            _poissonRatio = poissonRatio;
            _thickness = thickness;
        }

        public override void CalculateLocalSystem(out double[,] leftHandSideMatrix, out double[] rightHandSideVector)
        {
            CalculateStiffnessMatrix(out leftHandSideMatrix);
            
            var dof = GetDegreesOfFreedom();
            rightHandSideVector = new double[dof];
            
        }

        public override void CalculateMassMatrix(out double[,] massMatrix)
        {
            var dof = GetDegreesOfFreedom();
            massMatrix = new double[dof, dof];
            
            var density = 7850.0; // Steel density kg/m³
            var volume = Geometry.Volume();
            var totalMass = density * volume;
            var massPerNode = totalMass / Nodes.Count;
            
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < 3; j++) // 3 DOF per node (X, Y, Z)
                {
                    var index = i * 3 + j;
                    massMatrix[index, index] = massPerNode;
                }
            }
        }

        public override void CalculateStiffnessMatrix(out double[,] stiffnessMatrix)
        {
            var dof = GetDegreesOfFreedom();
            stiffnessMatrix = new double[dof, dof];
            
            
            var constitutiveMatrix = GetConstitutiveMatrix();
            
            
            var elementStiffness = _youngModulus * Geometry.Area() / Geometry.Volume();
            
            for (int i = 0; i < dof; i++)
            {
                stiffnessMatrix[i, i] = elementStiffness;
            }
            
            for (int i = 0; i < dof; i++)
            {
                for (int j = i + 1; j < dof; j++)
                {
                    var coupling = elementStiffness * 0.1; // Simplified coupling
                    stiffnessMatrix[i, j] = coupling;
                    stiffnessMatrix[j, i] = coupling;
                }
            }
        }

        private double[,] GetConstitutiveMatrix()
        {
            var factor = _youngModulus / (1.0 - _poissonRatio * _poissonRatio);
            var constitutive = new double[3, 3];
            
            constitutive[0, 0] = factor;
            constitutive[0, 1] = factor * _poissonRatio;
            constitutive[1, 0] = factor * _poissonRatio;
            constitutive[1, 1] = factor;
            constitutive[2, 2] = factor * (1.0 - _poissonRatio) / 2.0;
            
            return constitutive;
        }

        public override IElement Clone(int newId, IReadOnlyList<INode> newNodes)
        {
            return new SmallDisplacementElement(newId, Geometry, newNodes, _youngModulus, _poissonRatio, _thickness);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override string ToString()
        {
            return $"SmallDisplacementElement {Id}: E={_youngModulus:E2}, ν={_poissonRatio:F2}";
        }
    }
}
