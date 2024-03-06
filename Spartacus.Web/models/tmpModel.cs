using System.Collections.Generic;

namespace Spartacus.Web.Models
{
    public class tmpModel
    {
        /* exemplu model pentru abonament
         * id
         * nume
         * descriere
         * tipul abonamentului
         * filiala
         * image url
         * de la [pret]
        */
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Branch { get; set; }
        public List<string> Products { get; set; }

    }
}