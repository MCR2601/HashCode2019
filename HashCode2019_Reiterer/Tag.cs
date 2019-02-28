using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class Tag
    {
        public string Name;

        public Tag(string name)
        {
            Name = name;
        }
        public static bool operator ==(Tag a, Tag b)
        {
            return a.Name == b.Name;
        }
        public static bool operator !=(Tag a, Tag b)
        {
            return a.Name != b.Name;
        }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var tag = obj as Tag;
            return tag != null &&
                   Name == tag.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
    class TagComparere : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Tag obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
