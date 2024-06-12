using CQRS_Lib.CQRS.Commands;
using CQRS_Lib.Data;
using CQRS_Lib.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Lib.CQRS.Handelers
{
    public record InsertItemhandeler : IRequestHandler<InsertItemCommand, Items>
    {
        DBData dB;

        public InsertItemhandeler( DBData dB)
        {
            this.dB = dB;
        }

        public async Task<Items> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
          await  dB.AddAsync(request.item);
           await dB.SaveChangesAsync();
            return await Task.FromResult(request.item);
        }
    }
}
