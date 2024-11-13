
CREATE TABLE inventory.user
(
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(100) UNIQUE NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE inventory.location
(
    location_id SERIAL PRIMARY KEY,
    location_name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

CREATE TABLE inventory.item
(
    item_id SERIAL PRIMARY KEY,
    barcode VARCHAR(50) UNIQUE,
    item_name VARCHAR(255) UNIQUE NOT NULL,
    category VARCHAR(100),
    added_by INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (added_by) REFERENCES inventory.user(user_id) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE inventory.item_location
(
    item_location_id SERIAL PRIMARY KEY,
    item_id INT NOT NULL,
    location_id INT NOT NULL,
    quantity INT NOT NULL,
    last_updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (item_id) REFERENCES inventory.item(item_id) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (location_id) REFERENCES inventory.location(location_id) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE inventory.item_transaction
(
    transaction_id SERIAL PRIMARY KEY,
    item_location_id INT NOT NULL,
    user_id INT NOT NULL,
    quantity INT NOT NULL,
    transaction_type VARCHAR(50) NOT NULL,
    transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (item_location_id) REFERENCES inventory.item_location(item_location_id) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (user_id) REFERENCES inventory.user(user_id) ON UPDATE NO ACTION ON DELETE NO ACTION
);
