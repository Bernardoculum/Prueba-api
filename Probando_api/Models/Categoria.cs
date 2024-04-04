using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Probando_api.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }


        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        // esto para quitar el null
        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
