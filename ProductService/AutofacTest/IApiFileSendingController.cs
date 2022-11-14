using System.Collections.Generic;

namespace ProductService.AutofacTest
{
    public interface IApiFileSendingController
    {
        void Send(List<AftInvFileDTO> filesToSendRetry = null);
    }
}