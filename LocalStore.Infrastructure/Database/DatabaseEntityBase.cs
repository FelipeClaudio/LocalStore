using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalStore.Infrastructure.Database
{
    public class DatabaseEntityBase
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTime { get; set; }
    }
}
