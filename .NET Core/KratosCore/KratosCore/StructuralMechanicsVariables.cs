using System;

namespace KratosCore
{
    public static class StructuralMechanicsVariables
    {
        public static readonly Variable<double> DisplacementX = new Variable<double>("DISPLACEMENT_X", 0.0);
        public static readonly Variable<double> DisplacementY = new Variable<double>("DISPLACEMENT_Y", 0.0);
        public static readonly Variable<double> DisplacementZ = new Variable<double>("DISPLACEMENT_Z", 0.0);
        
        public static readonly Variable<double> VelocityX = new Variable<double>("VELOCITY_X", 0.0);
        public static readonly Variable<double> VelocityY = new Variable<double>("VELOCITY_Y", 0.0);
        public static readonly Variable<double> VelocityZ = new Variable<double>("VELOCITY_Z", 0.0);
        
        public static readonly Variable<double> AccelerationX = new Variable<double>("ACCELERATION_X", 0.0);
        public static readonly Variable<double> AccelerationY = new Variable<double>("ACCELERATION_Y", 0.0);
        public static readonly Variable<double> AccelerationZ = new Variable<double>("ACCELERATION_Z", 0.0);
        
        public static readonly Variable<double> ForceX = new Variable<double>("FORCE_X", 0.0);
        public static readonly Variable<double> ForceY = new Variable<double>("FORCE_Y", 0.0);
        public static readonly Variable<double> ForceZ = new Variable<double>("FORCE_Z", 0.0);
        
        public static readonly Variable<double> YoungModulus = new Variable<double>("YOUNG_MODULUS", 200e9);
        public static readonly Variable<double> PoissonRatio = new Variable<double>("POISSON_RATIO", 0.3);
        public static readonly Variable<double> Density = new Variable<double>("DENSITY", 7850.0);
        public static readonly Variable<double> Thickness = new Variable<double>("THICKNESS", 1.0);
        
        public static readonly Variable<double[]> Stress = new Variable<double[]>("STRESS", new double[6]);
        public static readonly Variable<double[]> Strain = new Variable<double[]>("STRAIN", new double[6]);
        
        public static readonly Variable<bool> IsFixed = new Variable<bool>("IS_FIXED", false);
        public static readonly Variable<bool> IsActive = new Variable<bool>("IS_ACTIVE", true);
    }
}
