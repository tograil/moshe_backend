﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GenericBackend.DataModels.Document;
using GenericBackend.Helpers;
using GenericBackend.Models;
using GenericBackend.Models.SettingsData;
using GenericBackend.UnitOfWork.GoodNightMedical;

namespace GenericBackend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult AddDocument(DocumentRequest request)
        {
            if (request == null)
                return BadRequest("document request can't be null");
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("document name can't be empty");
            var userName = UserModel.GetUserInfo(User).Name;
            var doc = new DocumentInfo
            {
                User = userName,
                DateOfPost = DateTime.UtcNow,
                Name = request.Name,
                Type = request.Type
            };
            _unitOfWork.DocumentsInfo.Add(doc);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            
            return Ok(await GetDocuments());
        }

        private Task<List<DocumentInfo>> GetDocuments()
        {
            var user = UserModel.GetUserInfo(User);
            var query = _unitOfWork.DocumentsInfo.AsQueryable();
            if (!user.IsSuperUser)
                query = _unitOfWork.DocumentsInfo.Where(x => x.User == user.Name);

            return Task.Factory.StartNew(() => query.ToList());
        }

        [HttpGet]
        [Route("plan/{id}")]
        public IHttpActionResult GetDocumentPlan(string id)
        {
            try
            {
                return Ok(SettingsModel.FromData(_unitOfWork.DocumentsInfo.GetById(id)));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }
    }
}
