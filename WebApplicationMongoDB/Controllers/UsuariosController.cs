using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebApplicationMongoDB.Data;
using WebApplicationMongoDB.Models;

namespace WebApplicationMongoDB.Controllers
{
    public class UsuariosController : Controller
    {
      
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            ContextMongodb contextMongodb = new ContextMongodb();
            return View(await contextMongodb.Usuario.Find(u=>true).ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongodb contextMongodb = new ContextMongodb();
            //compara o id recebido com o id do banco
            //se encontrar, tras o primeiro ou então um valor defaut
            var usuario = await contextMongodb.Usuario.Find(m => m.Id == id).FirstOrDefaultAsync(); 
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                ContextMongodb contextMongodb = new ContextMongodb();
                usuario.Id = Guid.NewGuid();
                await contextMongodb.Usuario.InsertOneAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongodb contextMongodb = new ContextMongodb();
            var usuario = await contextMongodb.Usuario.Find(u=>u.Id == id).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContextMongodb contextMongodb = new ContextMongodb();
                    //trocando o conteúdo do usuario desejado
                    await contextMongodb.Usuario.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongodb contextMongodb = new ContextMongodb();
            var usuario = await contextMongodb.Usuario.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            ContextMongodb contextMongodb = new ContextMongodb();
            await contextMongodb.Usuario.DeleteOneAsync(m => m.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            ContextMongodb contextMongodb = new ContextMongodb();
            return contextMongodb.Usuario.Find(e => e.Id == id).Any();
        }
    }
}
