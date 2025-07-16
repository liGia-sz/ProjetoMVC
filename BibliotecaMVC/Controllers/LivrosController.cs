using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMVCApp.Data; // Ajuste para o namespace do seu DbContext
using BibliotecaMVCApp.Models; // Ajuste para o namespace das suas Models

public class LivrosController : Controller
{
    private readonly BibliotecaDbContext _context;

    public LivrosController(BibliotecaDbContext context)
    {
        _context = context;
    }

    // GET: Livros
    public async Task<IActionResult> Index()
    {
        // Inclui o Autor relacionado para exibição
        var bibliotecaDbContext = _context.Livros.Include(l => l.Autor);
        return View(await bibliotecaDbContext.ToListAsync());
    }

    // GET: Livros/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros
            .Include(l => l.Autor) // Inclui o Autor
            .FirstOrDefaultAsync(m => m.Id == id);
        if (livro == null)
        {
            return NotFound();
        }

        return View(livro);
    }

    // GET: Livros/Create
    public IActionResult Create()
    {
        // Passa a lista de autores para o dropdown (SelectList)
        ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Nome");
        return View();
    }

    // POST: Livros/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Titulo,AnoPublicacao,Genero,FaixaEtaria,AutorId")] Livro livro)
    {
        if (ModelState.IsValid)
        {
            _context.Add(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Nome", livro.AutorId);
        return View(livro);
    }

    // GET: Livros/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return NotFound();
        }
        ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Nome", livro.AutorId);
        return View(livro);
    }

    // POST: Livros/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,AnoPublicacao,Genero,FaixaEtaria,AutorId")] Livro livro)
    {
        if (id != livro.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(livro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(livro.Id))
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
        ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Nome", livro.AutorId);
        return View(livro);
    }

    // GET: Livros/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros
            .Include(l => l.Autor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (livro == null)
        {
            return NotFound();
        }

        return View(livro);
    }

    // POST: Livros/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro != null)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool LivroExists(int id)
    {
        return _context.Livros.Any(e => e.Id == id);
    }
}