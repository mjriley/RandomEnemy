using System.Collections.Generic;

public class TileInfo
{
    private TerrainType m_terrainType;
    public TerrainType TileTerrain
    {
        get
        {
            return m_terrainType;
        }
    }
    
    private int m_hostilityThreshold;
    public int HostilityThreshold
    {
        get
        {
            return m_hostilityThreshold;
        }
    }
    
    private int m_hostilityIncrement;
    public int HostilityIncrement
    {
        get
        {
            return m_hostilityIncrement;
        }
    }
    
    List<PokemonData> m_pokemonData;
    public List<PokemonData> PokemonData
    {
        get
        {
            return m_pokemonData;
        }
    }

    public TileInfo(TerrainType terrainType, int hostilityThreshold, int hostilityIncrement, List<PokemonData> pokemonData)
    {
        m_terrainType = terrainType;
        m_hostilityThreshold = hostilityThreshold;
        m_hostilityIncrement = hostilityIncrement;
        m_pokemonData = pokemonData;
    }
}

