using BeymenCodeCase.Entity;
using BeymenCodeCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCodeCase.Services.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Logs> loggerRepository;

        public LoggerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.loggerRepository = unitOfWork.GetRepository<Logs>();
        }
        public void Insert(Logs logs)
        {
            throw new NotImplementedException();
        }
    }
}
