using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.AutofacTest
{
    public class ApiFileSendingController : ApiClientBase, IApiFileSendingController
    {
        private readonly IApiFlTester _apiFileTester;
       
        //public ApiFileSendingController(DTO dto, IApiFlTester tester) : base(dto)
        //{
        //    _tester = tester;
        //}

        public ApiFileSendingController(DTO dto) : base(dto)
        {
            _apiFileTester = GetApiTesterViaReflection();
        }


        public void Send(List<AftInvFileDTO> filesToSendRetry = null)
        {
            _apiFileTester.RegisterTestingMethods();
        }

        private IApiFlTester GetApiTesterViaReflection()
        {
            Type type = typeof(IApiFlTester).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IApiFlTester))).FirstOrDefault();
            return Activator.CreateInstance(type) as IApiFlTester;
        }

    }

    public interface IApiFlTester
    {
        void RegisterTestingMethods();
    }

    public class ApiFlTester : IApiFlTester
    {
        public void RegisterTestingMethods()
        {
            return; 
        }
    }

            
}
