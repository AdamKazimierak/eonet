# About project
- Project is using following RabbitMQ image: https://hub.docker.com/r/masstransit/rabbitmq
- Quartz job looks on interval for new events from https://eonet.gsfc.nasa.gov/docs/v3
- Quartz job pushes events to the queue
- MassTransit consumer processes the messages by sending API request to store these
- MassTransit is configured to use memory outbox
- MassTransit is configured to use retries
- API is idempotent and does not process the same event twice
- UI sends a request for paginated results, providing a date range as a criteria
- UI is built with React
- UI is compiled and bundled with Webpack
- UI is using Bootstrap 5, Bootstrap Icons, and Leafletjs

# Out of Scope
Due to time constrains, the following features are out of scope for this project:

- Authentication
- Geometry objects of type other than `Point`
- Unit Tests
- Domain
