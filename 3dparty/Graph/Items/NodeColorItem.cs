﻿/* Copyright (C) 2009-2017, NG Games Ltd. All rights reserved.

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

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph.Items
{
    public sealed class NodeColorItem : NodeItem
    {
        public event EventHandler<NodeItemEventArgs> Clicked;

        public NodeColorItem(string text, Color color, bool inputEnabled, bool outputEnabled) :
            base(inputEnabled, outputEnabled)
        {
            this.Text = text;
            this.Color = color;
        }

        public Color Color { get; set; }

        #region Text

        private string internalText = string.Empty;

        public string Text
        {
            get { return internalText; }
            set
            {
                if (internalText == value)
                    return;
                internalText = value;
                TextSize = Size.Empty;
            }
        }

        #endregion Text

        internal SizeF TextSize;

        public override bool OnClick()
        {
            base.OnClick();
            if (Clicked != null)
                Clicked(this, new NodeItemEventArgs(this));
            return true;
        }

        private const int ColorBoxSize = 16;
        private const int Spacing = 2;

        internal override SizeF Measure(Graphics graphics)
        {
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                if (this.TextSize.IsEmpty)
                {
                    var size = new Size(GraphConstants.MinimumItemWidth, GraphConstants.MinimumItemHeight);

                    if (this.Input.Enabled != this.Output.Enabled)
                    {
                        if (this.Input.Enabled)
                            this.TextSize = graphics.MeasureString(this.Text, SystemFonts.MenuFont, size, GraphConstants.LeftMeasureTextStringFormat);
                        else
                            this.TextSize = graphics.MeasureString(this.Text, SystemFonts.MenuFont, size, GraphConstants.RightMeasureTextStringFormat);
                    }
                    else
                        this.TextSize = graphics.MeasureString(this.Text, SystemFonts.MenuFont, size, GraphConstants.CenterMeasureTextStringFormat);

                    this.TextSize.Width = Math.Max(size.Width, this.TextSize.Width + ColorBoxSize + Spacing);
                    this.TextSize.Height = Math.Max(size.Height, this.TextSize.Height);
                }
                return this.TextSize;
            }
            else
            {
                return new SizeF(GraphConstants.MinimumItemWidth, GraphConstants.TitleHeight + GraphConstants.TopHeight);
            }
        }

        internal override void Render(Graphics graphics, SizeF minimumSize, PointF location)
        {
            var size = Measure(graphics);
            size.Width = Math.Max(minimumSize.Width, size.Width);
            size.Height = Math.Max(minimumSize.Height, size.Height);

            var alignment = HorizontalAlignment.Center;
            var format = GraphConstants.CenterTextStringFormat;
            if (this.Input.Enabled != this.Output.Enabled)
            {
                if (this.Input.Enabled)
                {
                    alignment = HorizontalAlignment.Left;
                    format = GraphConstants.LeftTextStringFormat;
                }
                else
                {
                    alignment = HorizontalAlignment.Right;
                    format = GraphConstants.RightTextStringFormat;
                }
            }

            var rect = new RectangleF(location, size);
            var colorBox = new RectangleF(location, size);
            colorBox.Width = ColorBoxSize;
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    rect.Width -= ColorBoxSize + Spacing;
                    rect.X += ColorBoxSize + Spacing;
                    break;

                case HorizontalAlignment.Right:
                    colorBox.X = rect.Right - colorBox.Width;
                    rect.Width -= ColorBoxSize + Spacing;
                    break;

                case HorizontalAlignment.Center:
                    rect.Width -= ColorBoxSize + Spacing;
                    rect.X += ColorBoxSize + Spacing;
                    break;
            }

            graphics.DrawString(this.Text, SystemFonts.MenuFont, Brushes.Black, rect, format);

            using (var path = GraphRenderer.CreateRoundedRectangle(colorBox.Size, colorBox.Location))
            {
                using (var brush = new SolidBrush(this.Color))
                {
                    graphics.FillPath(brush, path);
                }
                if ((state & RenderState.Hover) != 0)
                    graphics.DrawPath(Pens.White, path);
                else
                    graphics.DrawPath(Pens.Black, path);
            }
            //using (var brush = new SolidBrush(this.Color))
            //{
            //	graphics.FillRectangle(brush, colorBox.X, colorBox.Y, colorBox.Width, colorBox.Height);
            //}
            //graphics.DrawRectangle(Pens.Black, colorBox.X, colorBox.Y, colorBox.Width, colorBox.Height);
        }
    }
}