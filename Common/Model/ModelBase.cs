using Common.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Model
{
    public abstract class ModelBase
    {
        public int Id { get; set; }
    }
}
