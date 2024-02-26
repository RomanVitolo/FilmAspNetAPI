using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using FinalFilmProject.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalFilmProject.Controllers;

public class CustomBaseController : ControllerBase
{
     private readonly ApplicationDbContext _context;
     private readonly IMapper _mapper;

     public CustomBaseController(ApplicationDbContext context, IMapper mapper)
     {
          _context = context;
          _mapper = mapper;
     }

     protected async Task<List<TDTO>> Get<TEntity, TDTO>() where TEntity : class
     {
          var entities = await _context.Set<TEntity>().ToListAsync();
          var dtos = _mapper.Map<List<TDTO>>(entities);
          return dtos;
     }

     protected async Task<List<TDTO>> Get<TEntity, TDTO>(PageDTO pageDto) where TEntity : class
     {
          var queryable = _context.Set<TEntity>().AsQueryable(); 
          return await Get<TEntity, TDTO>(pageDto, queryable);
     }
     
     protected async Task<List<TDTO>> Get<TEntity, TDTO>(PageDTO pageDto, IQueryable<TEntity> queryable) where TEntity : class
     {    
          await HttpContext.InsertPageParameters(queryable, pageDto.RegistersPerPage);
          var entities = await queryable.Page(pageDto).ToListAsync();
          return _mapper.Map<List<TDTO>>(entities);
     }
     
     protected async Task<ActionResult<TDTO>> GetById<TEntity, TDTO>(int id) where TEntity : class, IId
     {
          var entity = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
          if (entity == null) NotFound();

          return _mapper.Map<TDTO>(entity);   
     }

     protected async Task<ActionResult> Post<TCreation, TEntity, TRead>(TCreation creationDTO, string routeName)
          where TEntity : class, IId
     {
          var entity = _mapper.Map<TEntity>(creationDTO);
          _context.Add(entity);
          await _context.SaveChangesAsync();

          var dtoRead = _mapper.Map<TRead>(entity);
        
          return new CreatedAtRouteResult(routeName, new {Id = entity.Id}, dtoRead); 
     }

     protected async Task<ActionResult> Put<TCreation, TEntity>(int id, TCreation creationDTO)
          where TEntity : class, IId
     {
          var entity = _mapper.Map<TEntity>(creationDTO);
          entity.Id = id;
          _context.Entry(entity).State = EntityState.Modified;
          await _context.SaveChangesAsync();
          return NoContent();
     }

     protected async Task<ActionResult> Patch<TEntity, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument)
          where TDTO : class where TEntity : class, IId
     {
          if (patchDocument == null) return BadRequest();

          var entityDB = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

          if (entityDB == null) return NotFound();

          var entityDTO = _mapper.Map<TDTO>(entityDB);
            
          patchDocument.ApplyTo(entityDTO, ModelState);

          var isValid = TryValidateModel(entityDTO);

          if (!isValid) return BadRequest(ModelState);

          _mapper.Map(entityDTO, entityDB);
          await _context.SaveChangesAsync();
          return NoContent();
     }  

     protected async Task<ActionResult> Delete<TEntity>(int id) where TEntity : class, IId, new()
     {
          var exist = await _context.Set<TEntity>().AnyAsync(x => x.Id == id);

          if (!exist) return NotFound();

          _context.Remove(new TEntity() {Id = id});
          await _context.SaveChangesAsync();

          return NoContent();
     }
}