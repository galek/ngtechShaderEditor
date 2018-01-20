/* Copyright (C) 2009-2017, NG Games Ltd. All rights reserved.

*
* This file is part of the NGTech (http://nggames.com/).
*
* Your use and or redistribution of this software in source and / or
* binary form, with or without modification, is subject to: (i) your
* ongoing acceptance of and compliance with the terms and conditions of
* the NGTech License Agreement; and (ii) your inclusion of this notice
* in any version of this software that you use or redistribute.
* A copy of the NGTech License Agreement is available by contacting
* NG Games Ltd. at http://nggames.com/
*/

#region License

// Copyright (c) 2009 Sander van Rossen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion License

using Graph.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Graph
{
    public sealed class NodeEventArgs : EventArgs
    {
        public NodeEventArgs(Node node)
        {
            Node = node;
        }

        public Node Node { get; private set; }
    }

    public sealed class ElementEventArgs : EventArgs
    {
        public ElementEventArgs(IElement element)
        {
            Element = element;
        }

        public IElement Element { get; private set; }
    }

    public sealed class AcceptNodeEventArgs : CancelEventArgs
    {
        public AcceptNodeEventArgs(Node node)
        {
            Node = node;
        }

        public AcceptNodeEventArgs(Node node, bool cancel) : base(cancel)
        {
            Node = node;
        }

        public Node Node { get; private set; }
    }

    public sealed class AcceptElementLocationEventArgs : CancelEventArgs
    {
        public AcceptElementLocationEventArgs(IElement element, Point position)
        {
            Element = element; Position = position;
        }

        public AcceptElementLocationEventArgs(IElement element, Point position, bool cancel) : base(cancel)
        {
            Element = element; Position = position;
        }

        public IElement Element { get; private set; }
        public Point Position { get; private set; }
    }

    [Serializable]
    public class Node : IElement
    {
        public string Title { get { return titleItem.Title; } set { titleItem.Title = value; } }
        public string Comments { get { return commentItem.Title; } set { commentItem.Title = value; } }

        #region Collapsed

        internal bool internalCollapsed;

        public bool Collapsed
        {
            get
            {
                return (internalCollapsed &&
                        ((state & RenderState.DraggedOver) == 0)) ||
                        nodeItems.Count == 0;
            }
            set
            {
                var oldValue = Collapsed;
                internalCollapsed = value;
                if (Collapsed != oldValue)
                {
                    titleItem.ForceResize();
                    commentItem.ForceResize();
                }
            }
        }

        #endregion Collapsed

        public bool HasNoItems { get { return nodeItems.Count == 0; } }

        // TODO: Add check on Locking position
        public PointF Location { get; set; }
        public object Tag { get; set; }

        public IEnumerable<NodeConnection> Connections { get { return connections; } }
        public IEnumerable<NodeItem> Items { get { return nodeItems; } }

        internal RectangleF bounds;
        internal RectangleF inputBounds;
        internal RectangleF outputBounds;
        internal RectangleF itemsBounds;
        internal RenderState state = RenderState.None;
        internal RenderState inputState = RenderState.None;
        internal RenderState outputState = RenderState.None;

        internal readonly List<NodeConnector> inputConnectors = new List<NodeConnector>();
        internal readonly List<NodeConnector> outputConnectors = new List<NodeConnector>();
        internal readonly List<NodeConnection> connections = new List<NodeConnection>();
        internal readonly NodeTitleItem titleItem = new NodeTitleItem();
        internal readonly NodeTitleItem commentItem = new NodeTitleItem();
        private readonly List<NodeItem> nodeItems = new List<NodeItem>();

        public Node(string title)
        {
            this.Title = title;
            titleItem.Node = this;
            commentItem.Node = this;
        }

        public void AddItem(NodeItem item)
        {
            if (nodeItems.Contains(item))
                return;
            if (item.Node != null)
                item.Node.RemoveItem(item);
            nodeItems.Add(item);
            item.Node = this;
        }

        public void RemoveItem(NodeItem item)
        {
            if (!nodeItems.Contains(item))
                return;
            item.Node = null;
            nodeItems.Remove(item);
        }

        // Returns true if there are some connections that aren't connected
        public bool AnyConnectorsDisconnected
        {
            get
            {
                foreach (var item in nodeItems)
                {
                    if (item.Input.Enabled && !item.Input.HasConnection)
                        return true;
                    if (item.Output.Enabled && !item.Output.HasConnection)
                        return true;
                }
                return false;
            }
        }

        // Returns true if there are some output connections that aren't connected
        public bool AnyOutputConnectorsDisconnected
        {
            get
            {
                foreach (var item in nodeItems)
                    if (item.Output.Enabled && !item.Output.HasConnection)
                        return true;
                return false;
            }
        }

        // Returns true if there are some input connections that aren't connected
        public bool AnyInputConnectorsDisconnected
        {
            get
            {
                foreach (var item in nodeItems)
                    if (item.Input.Enabled && !item.Input.HasConnection)
                        return true;
                return false;
            }
        }

        public ElementType ElementType { get { return ElementType.Node; } }
        public bool Locked { get { return m_Locked; } set { m_Locked = value; } }

        private bool m_Locked = false;
    }
}