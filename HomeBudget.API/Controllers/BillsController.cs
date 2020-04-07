using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HomeBudget.API.Data;
using HomeBudget.API.DTOs;
using HomeBudget.API.Helpers;
using HomeBudget.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeBudget.API.Controllers
{
    [Route("api/users/{userId}/bills")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        public readonly IBudgetRepository _repo;
        public readonly IMapper _mapper;
        public readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public BillsController(IBudgetRepository repository, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repository;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, 
            [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;
            photoForCreationDto.Description = photoForCreationDto.Description;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if(user.Photos == null)
                user.Photos = new List<Photo>();

            user.Photos.Add(photo);

            if(await _repo.SaveAll())
                {
                    var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                    return CreatedAtRoute("GetPhoto", new { userId = userId, id = photo.Id}, photoToReturn);
                }

                return BadRequest("Could not add the photo");

        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int userId, [FromQuery]DataFilter dateFilter)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photosFromRepo = await _repo.GetPhotos(userId, dateFilter);

            var photos = _mapper.Map<List<PhotoForReturnDto>>(photosFromRepo);

            return Ok(photos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id, int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

          

            var photoFromRepo = await _repo.GetPhoto(id);

            _repo.Delete(photoFromRepo);

            if(await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete income.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, int userId, PhotoForUpdateDto billForUpdate) {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(id);

            _mapper.Map(billForUpdate, photoFromRepo);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating bill {id} failed on save");

        }


    }
}