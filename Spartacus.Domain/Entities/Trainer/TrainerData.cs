using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Trainer
{
    public class TrainerData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Activity { get; set; }
        public string Bio {  get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string FileName { get; set; }
    }
}
