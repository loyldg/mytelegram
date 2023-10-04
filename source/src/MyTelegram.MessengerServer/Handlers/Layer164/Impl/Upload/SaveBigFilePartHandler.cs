//

//using MyTelegram.Schema.Upload;

//namespace MyTelegram.Handlers.Upload
//{
//    using System;
//    using System.IO;
//    using System.Collections;
//    using System.Threading.Tasks;
//    using MyTelegram.Schema;

//    public class SaveBigFilePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestSaveBigFilePart, IBool>,
//        Upload.ISaveBigFilePartHandler, IProcessedHandler
//    {
//        //private readonly IFileManager _fileManager;

//        //public SaveBigFilePartHandler(IFileManager fileManager)
//        //{
//        //    _fileManager = fileManager;
//        //}

//        protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
//            RequestSaveBigFilePart obj)
//        {
//            throw new NotImplementedException();
//            //await _fileManager.SaveFileAsync(input.UserId, obj.FileId, obj.FilePart, obj.Bytes);
//            //return new TBoolTrue();
//        }
//    }
//}


