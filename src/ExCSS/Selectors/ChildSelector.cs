﻿using System.IO;

namespace ExCSS
{
    public abstract class ChildSelector : StylesheetNode, ISelector
    {
        private readonly string _name;
        public int Step { get; private set; }
        public int Offset { get; private set; }
        protected ISelector Kind;

        protected ChildSelector(string name)
        {
            _name = name;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var a = Step.ToString();

            var b = string.Empty;
            if (Offset > 0)
                b = "+" + Offset;
            else if (Offset < 0)
                b = Offset.ToString();

            writer.Write(":{0}({1}n{2})", _name, a, b);
        }

        public Priority Specificity => Priority.OneClass;
        public string Text => this.ToCss();

        internal ChildSelector With(int step, int offset, ISelector kind)
        {
            Step = step;
            Offset = offset;
            Kind = kind;
            return this;
        }
    }
}