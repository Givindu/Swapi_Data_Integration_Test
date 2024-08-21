// src/App.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const App = () => {
    const [planets, setPlanets] = useState([]);
    const [ships, setShips] = useState([]);
    const [loading, setLoading] = useState(true);

    // Fetch data from the backend on component mount
    useEffect(() => {
        const fetchData = async () => {
            try {
                const planetResponse = await axios.get('http://localhost:5232/api/Data/planets');
                const shipResponse = await axios.get('http://localhost:5232/api/Data/ships');
                setPlanets(planetResponse.data);
                setShips(shipResponse.data);
            } catch (error) {
                console.error('Cannot fetching data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    // Handle changes in input fields
    const handleChange = (e, id, type) => {
        const { name, value } = e.target;
        if (type === 'planet') {
            setPlanets(prevPlanets =>
                prevPlanets.map(planet =>
                    planet.id === id ? { ...planet, [name]: value } : planet
                )
            );
        } else if (type === 'ship') {
            setShips(prevShips =>
                prevShips.map(ship =>
                    ship.id === id ? { ...ship, [name]: value } : ship
                )
            );
        }
    };

    // Submit modified data to the backend
    const handleSubmit = async () => {
        try {
            await axios.post('http://localhost:5232/api/Data/submit', { Planets: planets, Ships: ships });
            alert('Successfully.');
        } catch (error) {
            console.error('Failed:', error);
            alert('Failed');
        }
    };

    if (loading) return <p>Loading...</p>;

    return (
        <div>
            <h1>DxDy Data Integration Test</h1>

            <h2>Planets</h2>
            {planets.length === 0 ? (
                <p>No planets available.</p>
            ) : (
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Climate</th>
                            <th>Terrain</th>
                        </tr>
                    </thead>
                    <tbody>
                        {planets.map(planet => (
                            <tr key={planet.id}>
                                <td>{planet.id}</td>
                                <td><input name="name" value={planet.name} onChange={(e) => handleChange(e, planet.id, 'planet')} /></td>
                                <td><input name="climate" value={planet.climate} onChange={(e) => handleChange(e, planet.id, 'planet')} /></td>
                                <td><input name="terrain" value={planet.terrain} onChange={(e) => handleChange(e, planet.id, 'planet')} /></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            <h2>Ships</h2>
            {ships.length === 0 ? (
                <p>No ships available.</p>
            ) : (
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Model</th>
                            <th>Manufacturer</th>
                        </tr>
                    </thead>
                    <tbody>
                        {ships.map(ship => (
                            <tr key={ship.id}>
                                <td>{ship.id}</td>
                                <td><input name="name" value={ship.name} onChange={(e) => handleChange(e, ship.id, 'ship')} /></td>
                                <td><input name="model" value={ship.model} onChange={(e) => handleChange(e, ship.id, 'ship')} /></td>
                                <td><input name="manufacturer" value={ship.manufacturer} onChange={(e) => handleChange(e, ship.id, 'ship')} /></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}

            <button onClick={handleSubmit}>Submit Changes</button>
        </div>
    );
};

export default App;
