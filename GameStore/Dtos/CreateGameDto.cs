using System.ComponentModel.DataAnnotations;
using GameStore.Entities;

namespace GameStore.Dtos;

public record class CreateGameDto(
    [Required] [StringLength(50)] string Name,
    int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
