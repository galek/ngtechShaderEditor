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

namespace Graph.Compatibility
{
    /// <summary>
    /// Will behave as if all node item connectors aren't compatible.
    /// </summary>
    public class NeverCompatible : ICompatibilityStrategy
    {
        /// <summary>
        /// Determine if two node item connectors could be connected to each other.
        /// </summary>
        /// <param name="from">From which node connector are we connecting.</param>
        /// <param name="to">To which node connector are we connecting?</param>
        /// <returns><see langword="true"/> if the connection is valid; <see langword="false"/> otherwise</returns>
        public bool CanConnect(NodeConnector @from, NodeConnector to)
        {
            return false;
        }
    }
}