using System.Collections.Generic;
using CatCards.DAO;
using CatCards.Models;
using CatCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatCards.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatCardDao cardDao;
        private readonly ICatFactService catFactService;
        private readonly ICatPicService catPicService;
        

        public CatController(ICatCardDao cardDao, ICatFactService catFact, ICatPicService catPic)
        {
            this.catFactService = catFact;
            this.catPicService = catPic;
            this.cardDao = cardDao;
        }

        [HttpGet("random")]
        public ActionResult<CatCard> GetRandomCard()
        {
            CatCard catCard = new CatCard();
            catCard.CatFact = catFactService.GetFact().Text;
            catCard.ImgUrl = catPicService.GetPic().File;
            return catCard;
        }

        [HttpGet()]
        public ActionResult<List<CatCard>> GetAllCards()
        {
            List<CatCard> catCards = new List<CatCard>();
            catCards = cardDao.GetAllCards();
            return catCards;
        }

        [HttpGet("{id}")]
        public ActionResult<CatCard> GetCardById(int id)
        {
            CatCard catCard = new CatCard();
            catCard = cardDao.GetCard(id);
            return catCard;
        }

        [HttpPost]
        public ActionResult<CatCard> SaveCard(CatCard catCard)
        {

            CatCard existingCard = cardDao.SaveCard(catCard);

            return existingCard;
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCard(int id, CatCard catCard)
        {
            CatCard existingCard = cardDao.GetCard(id);
            if(existingCard == null)
            {
                return NotFound();
            }

            return Ok(cardDao.UpdateCard(catCard));
        }

        [HttpDelete("{id}")]
        public void DeleteCard(int id)
        {
            cardDao.RemoveCard(id);
        }
    }
}
