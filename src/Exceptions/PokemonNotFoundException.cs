using System;
/// <summary>
/// Exception thrown if pokemon name is not matched
/// </summary>
public class PokemonNotFoundException : Exception
{
    /// <summary>
    /// Empty Constructor
    /// </summary>
    public PokemonNotFoundException()
    {
    }
    /// <summary>
    /// Constructor with message
    /// </summary>
    public PokemonNotFoundException(string message)
        : base(message)
    {
    }
    /// <summary>
    /// Constructor with message and inner exception
    /// </summary>
    public PokemonNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}