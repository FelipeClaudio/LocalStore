using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalStore.Infrastructure.Database
{
    public class DatabaseEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTime { get; set; }
    }
}
