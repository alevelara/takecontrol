CREATE TABLE padel_club(
    padel_club_id int,
    padel_club_code varchar(100),
    name varchar(255),
    created_at datetime,
    PRIMARY KEY (padel_club_id),
    FOREIGN KEY (address_id) REFERENCES Address(address_id)
);

CREATE TABLE padel_club_user (
  padel_club_id int,
  user_id int,
  PRIMARY KEY (padel_club_id, user_id),
  FOREIGN KEY (user_id) REFERENCES User(user_id),
  FOREIGN KEY (padel_club_id) REFERENCES padel_club(padel_club_id),
);


CREATE TABLE User (
    user_id int,
    full_name varchar(255),
    created_at datetime,
    FOREIGN KEY (address_id) REFERENCES Address(address_id),
    PRIMARY KEY (user_id)
);

CREATE TABLE Address (
    address_id int,
    city varchar(255),
    province varchar(255),
    address varchar(255),
    PRIMARY KEY(address_id)
);

CREATE TABLE Match (
  match_id int,
  num_players int DEFAULT 1,
  padel_club_id int,
  match_type enum('tennis', 'padel'),
  status enum('reserved', 'free', 'pending'),
  date_match datetime,
  PRIMARY KEY(match_id)
);

CREATE TABLE match_user (
  match_id int,
  user_id int,
  PRIMARY KEY (match_id, user_id),
  FOREIGN KEY (match_id) REFERENCES Match(match_id),
  FOREIGN KEY (user_id) REFERENCES User(user_id),
);

/*
INSERT INTO Countries VALUES (1, 'USA', 350000);
INSERT INTO Countries VALUES (2, 'Mexico', 350001);
INSERT INTO Countries VALUES (3, 'Canada', 350002);
INSERT INTO Countries VALUES (4, 'Germany', 350002);
INSERT INTO Countries VALUES (5, 'Japan', 350002);

INSERT INTO Cities VAlUES (1, 'SQLVille', 1 , 1000); 
INSERT INTO Cities VAlUES (2, 'Mexico City', 2 , 1000); 
INSERT INTO Cities VAlUES (3, 'Toronto', 3 , 1000); 
INSERT INTO Cities VAlUES (4, 'Berlin', 4 , 1000); 
INSERT INTO Cities VAlUES (5, 'Tokyo', 5 , 1000);
 
INSERT INTO Persons VALUES (1, 'Chen', 'John', 'Alpha', 1, 1);
INSERT INTO Persons VALUES (2, 'Rilliet', 'Kai', 'Beta', 1, 1);
INSERT INTO Persons VALUES (3, 'Rhana', 'Omar', 'Charlie', 1, 1);
INSERT INTO Persons VALUES (4, 'Lee', 'Matt', 'Delta', 1, 1);


SELECT * FROM Countries;
SELECT * FROM Cities;
SELECT * FROM Persons;
*/