using System;

namespace Sample
{
    class Foo<TBar, TBaz>
    {
        public TBar Bar { get; set; }
        public TBaz Baz { get; set; }
    }

    class Qux<TQuux>
    {
        private class Foo<TBar, TBaz>
        {
            public TBar Bar { get; set; }
            public TBaz Baz { get; set; }
            public TQuux Quux { get; set; }
        }
    }
}
