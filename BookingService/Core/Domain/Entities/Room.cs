using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Price Price { get; set; }
        public bool InMaintenance { get; set; }

        public bool IsAvaliable
        {
            get
            {
                if (this.InMaintenance || this.HasGuest)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasGuest { 
            // verificar se existem bookins abertos para esta room
            get { return true; } 
        }

    }
}
