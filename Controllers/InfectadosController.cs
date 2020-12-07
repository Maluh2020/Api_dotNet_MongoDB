using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
     [ ApiController ]
    [ Route ( " [controlador] " )]
    public class InfectadosController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectados> _infectadosCollection;

        public InfectadosController (Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection <Infectados> (typeof(Infectados).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectados ([FromBody] InfectadosDto dto)
        {
            var infectado = new Infectados (dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado adicionado ocm sucesso");
        }

        [HttpGet]
        public  ActionResult  ObterInfectados ()
        {
            var  infectados  =  _infectadosCollection.Find(Builders<Infectados>.Filter.Empty).ToList();
            
            return  Ok ( infectados );
        }
        
    }
}