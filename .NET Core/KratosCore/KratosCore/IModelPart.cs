using System;
using System.Collections.Generic;

namespace KratosCore
{
    public interface IModelPart
    {
        string Name { get; }
        ICollection<INode> Nodes { get; }
        ICollection<IElement> Elements { get; }
        
        INode CreateNode(int id, double x = 0.0, double y = 0.0, double z = 0.0);
        void AddNode(INode node);
        void RemoveNode(int nodeId);
        INode GetNode(int nodeId);
        
        void AddElement(IElement element);
        void RemoveElement(int elementId);
        IElement GetElement(int elementId);
        
        void Clear();
        int GetNumberOfNodes();
        int GetNumberOfElements();
    }
}
