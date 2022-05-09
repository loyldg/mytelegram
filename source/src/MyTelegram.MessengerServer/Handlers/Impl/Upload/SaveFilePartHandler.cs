//

//using MyTelegram.Schema.Upload;

//namespace MyTelegram.Handlers.Upload
//{
//    using System;
//    using System.IO;
//    using System.Collections;
//    using System.Threading.Tasks;
//    using MyTelegram.Schema;

//    public class SaveFilePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestSaveFilePart, IBool>,
//        Upload.ISaveFilePartHandler, IProcessedHandler
//    {
//        //private readonly IFileManager _fileManager;

//        //public SaveFilePartHandler(IFileManager fileManager)
//        //{
//        //    _fileManager = fileManager;
//        //}

//        protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
//            RequestSaveFilePart obj)
//        {
//            throw new NotImplementedException();
//            //await _fileManager.SaveFileAsync(input.UserId, obj.FileId, obj.FilePart, obj.Bytes).ConfigureAwait(false);
//            //return new TBoolTrue();
//        }
//    }
//}


