import React, { Component } from 'react';
import axios from 'axios';

export class FetchCountryData extends Component {
    constructor() {
        super();
        this.state = {
            CountryData: []
        }
    }

    componentDidMount() {
        const token = sessionStorage.getItem('token');
        axios.get("https://localhost:44313/country", { headers: { "Authorization": `Bearer ${token}` } }).then(response => {
            this.setState({
                CountryData: response.data
            });
        });
    }

    render() {
        return (
            <section>
                <h1>Countries</h1>
                <div>
                    <table>
                        <thead><tr><th>Country</th><th>Alpha2</th><th>Alpah3</th><th>Numeric Code</th></tr></thead>
                        <tbody>
                            {
                                this.state.CountryData.map((p, index) => {
                                    return <tr key={index}><td>{p.Name}</td><td> {p.AlphaCode2}</td><td>{p.AlphaCode3}</td><td>{p.NumericCode}</td></tr>;
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </section>
        )
    }
}