﻿using System.Linq.Expressions;
using Domain.Artists;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class ArtistRepository : IArtistRepository
{
    private readonly ApplicationDbContext _context;

    public ArtistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Artist?> GetArtistByIdAsync(Guid id)
    {
        return _context.Artists
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public Task<Artist?> GetArtistByNameAsync(string name)
    {
        return _context.Artists
            .SingleOrDefaultAsync(a => a.Name == name);
    }

    public Task<bool> Any(Expression<Func<Artist, bool>> expression)
    {
        return _context.Artists.AnyAsync(expression);
    }

    public void Add(Artist artist)
    {
        _context.Artists.Add(artist);
    }

    public void Remove(Artist artist)
    {
        _context.Artists.Remove(artist);
    }
}