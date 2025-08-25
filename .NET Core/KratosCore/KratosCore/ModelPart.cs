using System;
using System.Collections.Generic;
using System.Linq;

namespace KratosCore
{
    public class ModelPart : IModelPart
    {
        private readonly Dictionary<int, INode> _nodes;
        private readonly Dictionary<int, IElement> _elements;

        public string Name { get; }
        public ICollection<INode> Nodes => _nodes.Values;
        public ICollection<IElement> Elements => _elements.Values;

        public ModelPart(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _nodes = new Dictionary<int, INode>();
            _elements = new Dictionary<int, IElement>();
        }

        public INode CreateNode(int id, double x = 0.0, double y = 0.0, double z = 0.0)
        {
            if (_nodes.ContainsKey(id))
                throw new ArgumentException($"Node with ID {id} already exists", nameof(id));

            var node = new Node(id, x, y, z);
            _nodes[id] = node;
            return node;
        }

        public void AddNode(INode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (_nodes.ContainsKey(node.Id))
                throw new ArgumentException($"Node with ID {node.Id} already exists", nameof(node));

            _nodes[node.Id] = node;
        }

        public void RemoveNode(int nodeId)
        {
            if (!_nodes.ContainsKey(nodeId))
                throw new ArgumentException($"Node with ID {nodeId} does not exist", nameof(nodeId));

            var elementsUsingNode = _elements.Values.Where(e => e.Nodes.Any(n => n.Id == nodeId)).ToList();
            if (elementsUsingNode.Any())
            {
                throw new InvalidOperationException($"Cannot remove node {nodeId} as it is used by {elementsUsingNode.Count} element(s)");
            }

            _nodes.Remove(nodeId);
        }

        public INode GetNode(int nodeId)
        {
            if (!_nodes.TryGetValue(nodeId, out var node))
                throw new ArgumentException($"Node with ID {nodeId} does not exist", nameof(nodeId));

            return node;
        }

        public void AddElement(IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (_elements.ContainsKey(element.Id))
                throw new ArgumentException($"Element with ID {element.Id} already exists", nameof(element));

            foreach (var node in element.Nodes)
            {
                if (!_nodes.ContainsKey(node.Id))
                    throw new ArgumentException($"Element references node {node.Id} which does not exist in this ModelPart", nameof(element));
            }

            _elements[element.Id] = element;
        }

        public void RemoveElement(int elementId)
        {
            if (!_elements.ContainsKey(elementId))
                throw new ArgumentException($"Element with ID {elementId} does not exist", nameof(elementId));

            _elements.Remove(elementId);
        }

        public IElement GetElement(int elementId)
        {
            if (!_elements.TryGetValue(elementId, out var element))
                throw new ArgumentException($"Element with ID {elementId} does not exist", nameof(elementId));

            return element;
        }

        public void Clear()
        {
            _elements.Clear();
            _nodes.Clear();
        }

        public int GetNumberOfNodes() => _nodes.Count;

        public int GetNumberOfElements() => _elements.Count;

        public override string ToString()
        {
            return $"ModelPart '{Name}': {GetNumberOfNodes()} nodes, {GetNumberOfElements()} elements";
        }
    }
}
