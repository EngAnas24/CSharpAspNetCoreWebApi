using CQRS_Lib.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Lib.CQRS.Commands
{
    public record InsertItemCommand(Items item) : IRequest<Items>;

}
