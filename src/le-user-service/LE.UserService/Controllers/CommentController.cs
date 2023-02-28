﻿using AutoMapper;
using LE.UserService.Dtos;
using LE.UserService.Models.Requests;
using LE.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LE.UserService.Controllers
{
    [Route("api/users/{id}/posts")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost("{postId}/comment/create")]
        public async Task<IActionResult> CreateComment(Guid id, Guid postId, CommentRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Empty request");

            var dto = _mapper.Map<CommentDto>(request);
            dto.UserId = id;
            dto.PostId = postId;
            var commentId = await _commentService.CreateComment(postId, dto, cancellationToken);
            return Ok(commentId);
        }

        [HttpPut("{postId}/comments/{commentId}/update")]
        public async Task<IActionResult> UpdateComment(Guid id, Guid postId, Guid commentId, CommentRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest("Empty request");
            if (!await _commentService.IsBelongToPost(postId, commentId, cancellationToken))
                return BadRequest("Comment is not belong to post");

            var dto = _mapper.Map<CommentDto>(request);
            await _commentService.UpdateComment(commentId, dto, cancellationToken);
            return Ok();
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetComments(Guid postId, CancellationToken cancellationToken = default)
        {
            var dtos = await _commentService.GetComments(postId, cancellationToken);
            return Ok(dtos);
        }

        [HttpDelete("{postId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComments(Guid postId, Guid commentId, CancellationToken cancellationToken = default)
        {
            if (!await _commentService.IsBelongToPost(postId, commentId, cancellationToken))
                return BadRequest("Comment is not belong to post");

            await _commentService.DeleteComment(commentId, cancellationToken);
            return Ok();
        }
    }
}