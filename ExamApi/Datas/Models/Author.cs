using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string? AuthorName { get; set; }
        public string? Description { get; set; }
        public List<EducationalMaterial>? EducationalMaterials { get; set; } = new List<EducationalMaterial>();

        public int? EducationalMaterialsCounter { get; set; }
    }
}
