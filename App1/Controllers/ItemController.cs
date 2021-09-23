using System;
using System.Collections.Generic;
using System.Linq;

using EntitiesAndModels;
using Reader;
using Writer;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        public ItemController(IReaderService readerService, IWriterService writerService)
        {
            this.readerService = readerService;
            this.writerService = writerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetItems> result = await readerService.GetAllItems().ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet("NoKK/{filter}")]
        public async Task<IActionResult> GetItemsByNoKK(string filter)
        {
            IEnumerable<GetItems> result = await readerService.GetItemsByNoKK(filter).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet("NamaKK/{filter}")]
        public async Task<IActionResult> GetItemsByNamaKK(string filter)
        {
            IEnumerable<GetItems> result = await readerService.GetItemsByNamaKK(filter).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet("NamaLengkap/{filter}")]
        public async Task<IActionResult> GetItemsByNamaLengkap(string filter)
        {
            IEnumerable<GetItems> result = await readerService.GetItemsByNamaLengkap(filter).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet("NIK/{filter}")]
        public async Task<IActionResult> GetItemsByNIK(string filter)
        {
            IEnumerable<GetItems> result = await readerService.GetItemsByNIK(filter).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost("ItemMain")]
        public async Task<IActionResult> PostNewItemMain([FromBody] PostItemMain api)
        {
            try
            {
                PostItemMain result = await writerService.PostItemMain(api).ConfigureAwait(false);
                if (result != null)
                {
                    return Ok(result);
                }

                throw new ArgumentException("Cannot add ItemMain");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPost("ItemDetail")]
        public async Task<IActionResult> PostNewItemDetail([FromBody] PostItemDetail api)
        {
            try
            {
                IEnumerable<GetItems> result = await writerService.PostItemDetail(api).ConfigureAwait(false);
                if (result != null)
                {
                    return Ok(result);
                }

                throw new ArgumentException("Cannot add ItemDetail");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPatch("ItemMain")]
        public async Task<IActionResult> PatchItemMain([FromBody] PatchItemMain api)
        {
            try
            {
                PatchItemMain result = await writerService.PatchItemMain(api).ConfigureAwait(false);
                if (result != null)
                {
                    return Ok(result);
                }

                throw new ArgumentException("Cannot edit ItemMain");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPatch("ItemDetail")]
        public async Task<IActionResult> PatchItemDetail([FromBody] PatchItemDetail api)
        {
            try
            {
                IEnumerable<GetItems> result = await writerService.PatchItemDetail(api).ConfigureAwait(false);
                if (result != null)
                {
                    return Ok(result);
                }

                throw new ArgumentException("Cannot edit ItemDetail");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpDelete("{ID}/ItemMain")]
        public async Task<IActionResult> DeleteItemMain(string ID)
        {
            try
            {
                await writerService.DeleteItemMain(ID).ConfigureAwait(false); ;

                return Ok();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpDelete("{ID}/ItemDetail")]
        public async Task<IActionResult> DeleteItemDetail(string ID)
        {
            try
            {
                await writerService.DeleteItemDetail(ID).ConfigureAwait(false); ;

                return Ok();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
