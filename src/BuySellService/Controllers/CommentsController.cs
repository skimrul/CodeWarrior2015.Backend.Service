﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using CW.Backend.DAL.CRUD.Contexts;
using CW.Backend.DAL.CRUD.Entities;
using CW.Backend.DAL.CRUD.Repositories;
using CW.Backend.DAL.CRUD.Repositories.Interfaces;
using EShopper.Models;
using Microsoft.AspNet.Identity;

namespace EShopper.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Comments")]
    public class CommentsController : ApiController {
        private ProductCRUDContext _context;
        private IProductCommentRepository _commentRepository;
        private IProductRepository _productRepository;

        public CommentsController() {
            _context = new ProductCRUDContext();
            _productRepository = new ProductRepository(_context);
        }

        [Authorize]
        [Route("Save")]
        public HttpResponseMessage Save(ProductCommentDetailsViewModel comment) {
            var userId = Thread.CurrentPrincipal.Identity.GetUserId();
            var product = _productRepository.GetAllIncluding(p => p.Comments).First(p => p.Id == comment.ProductId);
            product.Comments.Add(new ProductComment {
                Comment = comment.Comment,
                StarRating = comment.StarRating,
                HelpfulHits = comment.HelpfulHits,
                ProductId = comment.ProductId,
                ApplicationUserId = userId
            });

            _productRepository.Update(product);
            _productRepository.Save();

            var msg = "Comment Successfully added";
            var response = Request.CreateResponse(msg);

            return response;
        }
    }
}
