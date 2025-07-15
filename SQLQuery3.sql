SELECT*FROM Movies
SELECT*FROM Actors
SELECT*FROM Directors
SELECT*FROM Genres
SELECT*FROM MovieGenres
SELECT*FROM MovieActors
SELECT*FROM Rooms
SELECT*FROM Seats
SELECT*FROM SeatTypes
SELECT*FROM showtimes

INSERT INTO SeatTypes ([Name], ExtraFee, IsActive)
VALUES 
  ('Ghế Thường', 15000, 1),
  ('Ghế VIP', 20000, 1),
  ('Ghế Đôi', 30000, 1)
  
