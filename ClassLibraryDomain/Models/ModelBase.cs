using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Models
{
    public abstract class ModelBase
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
