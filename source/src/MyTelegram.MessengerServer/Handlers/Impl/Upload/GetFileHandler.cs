//

//using Microsoft.Extensions.Logging;
//using MyTelegram.Domain;
////using MyTelegram.Exceptions;
//using MyTelegram.Queries.MongoDB.File;
//using MyTelegram.ReadModel.MongoDB;
//using MyTelegram.Schema.Storage;
//using MyTelegram.Schema.Upload;

//namespace MyTelegram.Handlers.Upload
//{
//    using System;
//    using System.IO;
//    using System.Collections;
//    using System.Threading.Tasks;
//    using MyTelegram.Schema;

//    public class GetFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetFile, MyTelegram.Schema.Upload.IFile>,
//        Upload.IGetFileHandler, IProcessedHandler
//    {
//        //private readonly IFileManager _fileManager;
//        //private readonly ILogger<GetFileHandler> _logger;

//        //public GetFileHandler(IFileManager fileManager,
//        //    ILogger<GetFileHandler> logger)
//        //{
//        //    _fileManager = fileManager;
//        //    _logger = logger;
//        //}

//        protected override async Task<IFile> HandleCoreAsync(IRequestInput input,
//            RequestGetFile obj)
//        {
//            throw new NotImplementedException();
//            //var getFileInput = new GetFileInput
//            //{
//            //    Limit = obj.Limit,
//            //    Offset = obj.Offset,
//            //    NeedReadFileData = true
//            //};
//            //IFileType fileType;
//            //long fileId;
//            //switch (obj.Location)
//            //{
//            //    case Schema.LayerN.Entities.TInputPeerPhotoFileLocation inputPeerPhotoFileLocation:
//            //        fileId = inputPeerPhotoFileLocation.VolumeId;

//            //        fileType = new TFileJpeg();
//            //        break;
//            //    case TInputDocumentFileLocation inputDocumentFileLocation:
//            //        fileId = inputDocumentFileLocation.Id;
//            //        fileType = new TFilePartial();
//            //        break;
//            //    case TInputEncryptedFileLocation inputEncryptedFileLocation:
//            //        fileId = inputEncryptedFileLocation.Id;
//            //        fileType = new TFilePartial();
//            //        break;
//            //    case TInputFileLocation inputFileLocation:
//            //        fileId = inputFileLocation.VolumeId;
//            //        fileType = new TFilePartial();
//            //        if (fileId == 0)
//            //        {
//            //            getFileInput.FileReference = inputFileLocation.FileReference;
//            //        }
//            //        break;
//            //    //case TInputGroupCallStream inputGroupCallStream:

//            //    //    break;
//            //    case TInputPeerPhotoFileLocation inputPeerPhotoFileLocation1:
//            //        fileId = inputPeerPhotoFileLocation1.PhotoId;
//            //        fileType = new TFileJpeg();
//            //        break;
//            //    case TInputPhotoFileLocation inputPhotoFileLocation:
//            //        fileId = inputPhotoFileLocation.Id;
//            //        fileType = new TFileJpeg();
//            //        break;
//            //    case TInputPhotoLegacyFileLocation inputPhotoLegacyFileLocation:
//            //        fileId = inputPhotoLegacyFileLocation.Id;
//            //        fileType = new TFileJpeg();
//            //        break;
//            //    case TInputSecureFileLocation inputSecureFileLocation:
//            //        fileId = inputSecureFileLocation.Id;
//            //        fileType = new TFileUnknown();
//            //        break;
//            //    //case TInputStickerSetThumb inputStickerSetThumb:

//            //    //    break;
//            //    //case TInputTakeoutFileLocation inputTakeoutFileLocation:

//            //    //    break;
//            //    default:
//            //        throw new ArgumentOutOfRangeException(nameof(obj), $"Unsupported location {obj.Location}");
//            //}

//            ////if (isSupportedLocation)
//            //{
//            //    getFileInput.FileId = fileId;
//            //    var file = await _fileManager.GetFileAsync(getFileInput);
//            //    if (file == null)
//            //    {
//            //        throw new BadRequestException("INVALID_FILE_ID");
//            //    }

//            //    if (!string.IsNullOrEmpty(file.MimeType) && file.MimeType.Contains(MyTelegramDomainConsts.VideoMimeType))
//            //    {
//            //        if (string.IsNullOrEmpty(getFileInput.ThumbSize))
//            //        {
//            //            fileType = new TFileMp4();
//            //        }
//            //        else
//            //        {
//            //            fileType = new TFileJpeg();
//            //        }
//            //    }

//            //    _logger.LogDebug($"[{file.Bytes.Length}/{file.Size}]Offset={obj.Offset} Limit={obj.Limit}, get file success:{fileId} serverId={file.ServerFileId},fileType:{fileType?.GetType().Name} size:{file.Size}");

//            //    IFile r = new TFile
//            //    {
//            //        Bytes = file.Bytes.Slice(0, file.ReadCount).ToArray(),
//            //        Type = fileType ?? new TFileUnknown(),
//            //        Mtime = file.Date
//            //    };

//            //    return r;
//            //}

//            ////throw new NotSupportedException($"not supported file location :{obj.Location.GetType().Name}");
//        }
//    }
//}


