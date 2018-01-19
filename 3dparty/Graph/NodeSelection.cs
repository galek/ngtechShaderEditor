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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    [Serializable]
    public class NodeSelection : IElement
    {
        public NodeSelection(IEnumerable<Node> nodes)
        {
            Nodes = nodes.ToArray();
        }

        public ElementType ElementType { get { return ElementType.NodeSelection; } }
        public readonly Node[] Nodes;
    }
}