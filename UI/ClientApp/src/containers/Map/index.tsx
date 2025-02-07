import "./index.less";
import "react-bootstrap/"
import { useState, memo, useRef, useEffect } from 'react';
import Map from './components/Map'
import axios from 'axios';
import { FeatureCollection } from 'geojson';
import { Form, Row, Col, InputGroup, Button } from 'react-bootstrap';

const MemoMap = memo(Map);
const defaultResults: FeatureCollection = {
    type: "FeatureCollection",
    features: []
}

function getDate(addDays: number = 0) {
    const date = new Date();
    date.setDate(date.getDate() + addDays);
    return date;
}

function getDateString(date: Date) {
    return date.toISOString().split('T')[0];
}

export default function MapContainer() {
    const page = useRef(1);
    const [loading, setLoading] = useState(false);
    const [startDate, setStartDate] = useState(getDateString(getDate(-5)));
    const [endDate, setEndDate] = useState(getDateString(getDate()));
    const [collection, setCollection] = useState<FeatureCollection>(defaultResults);

    const load = () => {
        setLoading(true);
        axios.get<FeatureCollection>(`http://localhost:5224/api/v1/features?startUtc=${startDate}&endUtc=${endDate}&page=${page.current}`)
            .then(d => d.data)
            .then(d => {
                setCollection({
                    ...d,
                    features: [...collection.features, ...d.features]
                })
                page.current++;
            }).finally(() => setLoading(false));
    };

    const reset = () => {
        page.current = 1;
        setCollection(defaultResults);
    }

    useEffect(reset, [startDate, endDate]);

    return <main>
        <Form>
            <Row className="align-items-center">
                <Col sm={3} className="my-1">
                    <Form.Label htmlFor="start-date" visuallyHidden>
                        Start Date
                    </Form.Label>

                    <InputGroup className="mb-2">
                        <InputGroup.Text>Start Date</InputGroup.Text>

                        <Form.Control
                            id="start-date"
                            type="date"
                            value={startDate}
                            onChange={v => setStartDate(v.target.value)}
                        />

                    </InputGroup>

                </Col>

                <Col sm={3} className="my-1">
                    <Form.Label htmlFor="end-date" visuallyHidden>
                        End Date
                    </Form.Label>

                    <InputGroup className="mb-2">
                        <InputGroup.Text>End Date</InputGroup.Text>

                        <Form.Control
                            id="end-date"
                            type="date"
                            value={endDate}
                            onChange={v => setEndDate(v.target.value)}
                        />

                    </InputGroup>

                </Col>

                <Col xs="auto" className="my-1">
                    <Button type="button" onClick={load} disabled={loading}>
                        {
                            collection.features.length ? "Load More" : "Load Events"
                        }

                    </Button>

                </Col>

                <Col xs="auto" className="my-1">
                    <Button onClick={reset} className="btn btn-light" type="button" disabled={loading}>Reset</Button>

                </Col>

            </Row>

        </Form>

        <MemoMap featureCollection={collection} />

    </main>
}