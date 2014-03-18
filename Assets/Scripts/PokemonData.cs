public struct PokemonData
{
    public Pokemon pokemon;
    public uint weight;
    
    public PokemonData(Pokemon pokemon, uint weight)
    {
        this.pokemon = pokemon;
        this.weight = weight;
    }
}