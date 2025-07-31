USE QuanLyQuanAn;

-- Bảng Account (quản lý tài khoản)
-- $2a$11$kQFeIDxIrSqpMFLcEk2tOO85SmV2F4lDOF5/be/0kM00NgYciSQeS 
CREATE TABLE Account (
    UserName NVARCHAR(100) PRIMARY KEY,
    PassWord NVARCHAR(400) NOT NULL,
    Type INT NOT NULL DEFAULT 0, -- 0: staff, 1: admin
    isActive BIT NOT NULL DEFAULT 1 -- 0: inactive, 1: active
);
ALTER TABLE Account ALTER COLUMN PassWord NVARCHAR(400) NOT NULL;

-- Bảng Staff (quản lý thông tin nhân viên)
CREATE TABLE Staff (
    idStaff INT IDENTITY(1,1) PRIMARY KEY,
    fullName NVARCHAR(100) NOT NULL,
    gender NVARCHAR(10),
    birthDate DATE,
    phone NVARCHAR(15),
    email NVARCHAR(100),
    accountUserName NVARCHAR(100) UNIQUE NULL,

    FOREIGN KEY (accountUserName) REFERENCES Account(UserName)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- Bảng TableFood (quản lý thông tin bàn ăn)
CREATE TABLE TableFood (
    idTable INT IDENTITY(1,1) PRIMARY KEY,
    tableName NVARCHAR(100) NOT NULL,
    status NVARCHAR(100) NOT NULL DEFAULT N'Trống' -- Trống / Có người
);

-- Bảng Bill (quản lý hóa đơn)
CREATE TABLE Bill (
    idBill INT IDENTITY(1,1) PRIMARY KEY,
    DateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATETIME,
    idTable INT NOT NULL,
    status INT NOT NULL DEFAULT 0, -- 0: chưa thanh toán, 1: đã thanh toán
    createdBy NVARCHAR(100) NULL, -- Nhân viên tạo bill

    FOREIGN KEY (idTable) REFERENCES TableFood(idTable)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (createdBy) REFERENCES Staff(accountUserName)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- Bảng FoodCategory (danh mục món ăn)
CREATE TABLE FoodCategory (
    idCategory INT IDENTITY(1,1) PRIMARY KEY,
    categoryName NVARCHAR(200) NOT NULL
);

-- Bảng Food (thông tin món ăn)
CREATE TABLE Food (
    idFood INT IDENTITY(1,1) PRIMARY KEY,
    foodName NVARCHAR(200) NOT NULL,
    idCategory INT NOT NULL,
    price DECIMAL(10,2) NOT NULL DEFAULT 0,

    FOREIGN KEY (idCategory) REFERENCES FoodCategory(idCategory)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- Bảng BillInfo (chi tiết hóa đơn)
CREATE TABLE BillInfo (
    idBill INT NOT NULL,
    idFood INT NOT NULL,
    count INT NOT NULL DEFAULT 1,

    PRIMARY KEY (idBill, idFood),
    FOREIGN KEY (idBill) REFERENCES Bill(idBill)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (idFood) REFERENCES Food(idFood)
        ON DELETE CASCADE ON UPDATE CASCADE
);


-- Bảng Ingredient (nguyên liệu)
CREATE TABLE Ingredient (
    idIngredient INT IDENTITY(1,1) PRIMARY KEY,
    ingredientName NVARCHAR(100) NOT NULL,
    unit NVARCHAR(50) NOT NULL,
    quantity DECIMAL(10,3) NOT NULL DEFAULT 0
);

-- Bảng FoodIngredient (định lượng nguyên liệu)
CREATE TABLE FoodIngredient (
    idFood INT NOT NULL,
    idIngredient INT NOT NULL,
    quantity DECIMAL(10,3) NOT NULL,

    PRIMARY KEY (idFood, idIngredient),
    FOREIGN KEY (idFood) REFERENCES Food(idFood)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (idIngredient) REFERENCES Ingredient(idIngredient)
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Supplier (
    idSupplier INT IDENTITY(1,1) PRIMARY KEY,
    supplierName NVARCHAR(200) NOT NULL,
    phone NVARCHAR(20),
    email NVARCHAR(100),
    address NVARCHAR(300)
);

CREATE TABLE ImportReceipt (
    idReceipt INT IDENTITY(1,1) PRIMARY KEY,
    importDate DATETIME NOT NULL DEFAULT GETDATE(),
    importedBy NVARCHAR(100) NULL,       -- Phải cho phép NULL để dùng SET NULL
    idSupplier INT NULL,                 -- Phải cho phép NULL để dùng SET NULL

    FOREIGN KEY (importedBy) REFERENCES Staff(accountUserName)
        ON DELETE SET NULL ON UPDATE CASCADE,
    FOREIGN KEY (idSupplier) REFERENCES Supplier(idSupplier)
        ON DELETE SET NULL ON UPDATE CASCADE
);



CREATE TABLE ImportDetail (
    idReceipt INT NOT NULL,
    idIngredient INT NOT NULL,
    quantity DECIMAL(10,3) NOT NULL,
    unitPrice DECIMAL(10,2) NOT NULL, -- giá nhập từng nguyên liệu

    PRIMARY KEY (idReceipt, idIngredient),
    FOREIGN KEY (idReceipt) REFERENCES ImportReceipt(idReceipt)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (idIngredient) REFERENCES Ingredient(idIngredient)
        ON DELETE CASCADE ON UPDATE CASCADE
);


-- Account
INSERT INTO Account (UserName, PassWord, Type, isActive)
VALUES 
(N'admin', N'$2a$11$kQFeIDxIrSqpMFLcEk2tOO85SmV2F4lDOF5/be/0kM00NgYciSQeS', 1, 1),
(N'staff01', N'$2a$11$kQFeIDxIrSqpMFLcEk2tOO85SmV2F4lDOF5/be/0kM00NgYciSQeS', 0, 1),
(N'staff02', N'$2a$11$kQFeIDxIrSqpMFLcEk2tOO85SmV2F4lDOF5/be/0kM00NgYciSQeS', 0, 1);

-- Staff
INSERT INTO Staff (fullName, gender, birthDate, phone, email, accountUserName)
VALUES 
(N'Trần Chí Hiếu', N'Nam', '2004-01-01', '090136971', 'tranchihieu3600@gmail.com', 'admin'),
(N'Nguyễn Công Minh', N'Nam', '2004-10-23', '0902345678', 'minh@gmail.com', 'staff01'),
(N'Nguyễn Chánh Hào', N'Nam', '1998-10-01', '0903456789', 'hao@gmail.com', 'staff02');

INSERT INTO TableFood (tableName, status)
VALUES 
(N'Bàn 1', N'Trống'),
(N'Bàn 2', N'Trống'),
(N'Bàn 3', N'Trống'),
(N'Bàn 4', N'Trống'),
(N'Bàn 5', N'Trống');

INSERT INTO TableFood (tableName, status)
VALUES 
(N'Bàn 6', N'Trống'),
(N'Bàn 7', N'Trống'),
(N'Bàn 8', N'Trống'),
(N'Bàn 9', N'Trống'),
(N'Bàn 10', N'Trống');

INSERT INTO TableFood (tableName, status)
VALUES 
(N'Bàn Vip 1', N'Trống'),
(N'Bàn Vip 2', N'Trống'),
(N'Bàn Vip 3', N'Trống'),
(N'Bàn Vip 4', N'Trống'),
(N'Bàn Vip 5', N'Trống');

INSERT INTO FoodCategory (categoryName)
VALUES 
(N'Món Cơm'),
(N'Món Bún - Mì - Phở'),
(N'Món Lẩu'),
(N'Món Chiên'),
(N'Món Xào'),
(N'Món Hấp'),
(N'Món Canh'),
(N'Món Chay'),
(N'Nước Uống'),
(N'Món Tráng Miệng');

-- Món Cơm (1)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Cơm Tấm Sườn Bì Chả', 1, 45000),
(N'Cơm Gà Xối Mỡ', 1, 40000);

-- Món Bún - Mì - Phở (2)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Phở Bò', 2, 40000),
(N'Bún Bò Huế', 2, 38000),
(N'Mì Xào Hải Sản', 2, 39000);

-- Món Lẩu (3)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Lẩu Thái Hải Sản', 3, 250000),
(N'Lẩu Gà Lá É', 3, 230000);

-- Món Chiên (4)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Cá Chiên Sốt Cà', 4, 40000),
(N'Gà Chiên Nước Mắm', 4, 45000),
(N'Chả Giò', 4, 25000);

-- Món Xào (5)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Bò Xào Rau Muống', 5, 50000),
(N'Mực Xào Sa Tế', 5, 55000);

-- Món Hấp (6)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Tôm Hấp Nước Dừa', 6, 60000),
(N'Trứng Hấp Thịt Bằm', 6, 35000);

-- Món Canh (7)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Canh Chua Cá Lóc', 7, 45000),
(N'Canh Rau Ngót Thịt Bằm', 7, 30000);

-- Món Chay (8)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Cơm Chay Thập Cẩm', 8, 35000),
(N'Đậu Hũ Kho Tộ', 8, 30000);

-- Nước Uống (9)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Trà Đá', 9, 0),
(N'Nước Mía', 9, 10000),
(N'Nước Suối', 9, 5000),
(N'Coca Cola', 9, 10000),
(N'Pessi', 9, 10000),
(N'Trà Tắc Đống Chai', 9, 8000);

-- Món Tráng Miệng (10)
INSERT INTO Food (foodName, idCategory, price) VALUES
(N'Kem Sữa', 10, 15000),
(N'Bánh Flan', 10, 12000),
(N'Rau Câu Dừa', 10, 10000),
(N'Kem Chuối', 10, 10000);


-- Thêm nguyên liệu cơ bản
INSERT INTO Ingredient (ingredientName, unit, quantity) VALUES
(N'Gạo tấm', N'gram', 10000),
(N'Sườn heo', N'gram', 5000),
(N'Bì heo', N'gram', 3000),
(N'Chả trứng', N'cái', 1000),
(N'Thịt gà', N'gram', 4000),
(N'Thịt bò', N'gram', 5000),
(N'Tôm', N'gram', 4000),
(N'Mực', N'gram', 3000),
(N'Cá', N'gram', 3000),
(N'Phở', N'gram', 3000),
(N'Bún', N'gram', 3000),
(N'Dưa leo', N'gram', 2000),
(N'Nấm', N'gram', 2000),
(N'Rau sống', N'gram', 3000),
(N'Rau muống', N'gram', 2000),
(N'Rau ngót', N'gram', 2000),
(N'Nước mắm', N'ml', 5000),
(N'Nước tương', N'ml', 3000),
(N'Nước sốt cà', N'ml', 1000),
(N'Sa Tế', N'ml', 1000),
(N'Dầu ăn', N'ml', 10000),
(N'Muối', N'gram', 10000),
(N'Tiêu', N'gram', 500),
(N'Đường', N'gram', 5000),
(N'Tỏi', N'gram', 2000),
(N'Hành lá', N'gram', 2000),
(N'Nước lọc', N'ml', 10000),
(N'Nước dừa', N'ml', 2000),
(N'Đậu hũ', N'miếng', 1000),
(N'Bánh flan', N'cái', 500),
(N'Rau câu', N'gram', 1000),
(N'Sữa đặc', N'ml', 1000),
(N'Trà đá', N'chai', 500),
(N'Nước mía', N'chai', 500),
(N'Nước suối', N'chai', 1000),
(N'Coca Cola', N'chai', 1000),
(N'Pepsi', N'chai', 1000),
(N'Trà tắc', N'chai', 500);


DELETE FROM Ingredient;
DBCC CHECKIDENT ('Ingredient', RESEED, 0);
-- Định lượng nguyên liệu:

-- Xóa dữ liệu cũ nếu có
DELETE FROM FoodIngredient;
DBCC CHECKIDENT ('FoodIngredient', RESEED, 0);

-- Thêm dữ liệu đầy đủ cho tất cả món ăn
INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES

 -- CƠM TẤM SƯỜN BÌ CHẢ (Món chính - chế biến phức tạp)
(1, 1, 200.000),  -- Gạo tấm: 200g
(1, 2, 100.000),  -- Sườn heo: 100g
(1, 3, 50.000),   -- Bì heo: 50g
(1, 4, 1.000),    -- Chả trứng: 1 cái
(1, 17, 10.000),  -- Nước mắm: 10ml
(1, 21, 5.000),   -- Dầu ăn: 5ml
(1, 22, 2.000),   -- Muối: 2g
(1, 23, 1.000),   -- Tiêu: 1g
(1, 24, 5.000),   -- Đường: 5g
(1, 25, 5.000);   -- Tỏi: 5g

/**********************************************************
 * 2. CƠM GÀ XỐI MỠ (Món chính)
 * Nguyên liệu chế biến: Định lượng theo gram/ml
 **********************************************************/
 INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES
(2, 1, 200.000),  -- Gạo tấm: 200g
(2, 5, 150.000),  -- Thịt gà: 150g
(2, 17, 10.000),  -- Nước mắm: 10ml
(2, 21, 20.000),  -- Dầu ăn: 20ml
(2, 22, 2.000),   -- Muối: 2g
(2, 23, 1.000),   -- Tiêu: 1g
(2, 24, 5.000),   -- Đường: 5g
(2, 25, 5.000);   -- Tỏi: 5g

 -- 3. Phở Bò
 INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES
(3, 10, 150.000), (3, 6, 100.000), (3, 22, 3.000), (3, 23, 2.000),
(3, 24, 10.000), (3, 25, 10.000), (3, 26, 10.000), (3, 27, 300.000);

-- 4. Bún Bò Huế
INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES

(4, 11, 150.000), (4, 6, 120.000), (4, 19, 20.000), (4, 20, 10.000),
(4, 22, 3.000), (4, 24, 15.000), (4, 25, 15.000), (4, 26, 15.000),
(4, 27, 300.000),

-- 5. Mì Xào Hải Sản
(5, 7, 80.000), (5, 8, 80.000), (5, 12, 30.000), (5, 13, 20.000),
(5, 18, 15.000), (5, 21, 20.000), (5, 24, 10.000), (5, 25, 10.000),

-- 6. Lẩu Thái Hải Sản
(6, 7, 200.000), (6, 8, 200.000), (6, 9, 150.000), (6, 12, 50.000),
(6, 13, 50.000), (6, 14, 100.000), (6, 20, 30.000), (6, 21, 30.000),
(6, 22, 10.000), (6, 24, 30.000), (6, 25, 20.000),

-- 7. Lẩu Gà Lá É
(7, 5, 300.000), (7, 13, 50.000), (7, 14, 100.000), (7, 21, 30.000),
(7, 22, 10.000), (7, 24, 20.000), (7, 25, 20.000), (7, 26, 20.000);

-- 8. Cá Chiên Sốt Cà
INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES
(8, 9, 200.000), (8, 19, 50.000), (8, 21, 30.000), (8, 22, 5.000),
(8, 24, 15.000), (8, 25, 10.000),

-- 9. Gà Chiên Nước Mắm
(9, 5, 200.000), (9, 17, 30.000), (9, 21, 50.000), (9, 22, 5.000),
(9, 24, 20.000), (9, 25, 15.000),

-- 10. Chả Giò
(10, 5, 50.000), (10, 12, 30.000), (10, 13, 20.000), (10, 21, 100.000),
(10, 22, 3.000), (10, 24, 5.000), (10, 25, 10.000);

-- 11. Bò Xào Rau Muống
INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES

(11, 6, 150.000), (11, 15, 100.000), (11, 17, 15.000), (11, 21, 30.000),
(11, 22, 3.000), (11, 24, 10.000), (11, 25, 15.000),

-- 12. Mực Xào Sa Tế
(12, 8, 200.000), (12, 20, 20.000), (12, 21, 30.000), (12, 22, 3.000),
(12, 24, 10.000), (12, 25, 15.000);

-- 13. Tôm Hấp Nước Dừa
INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES

(13, 7, 250.000), (13, 28, 100.000), (13, 22, 5.000), (13, 23, 2.000),
(13, 25, 10.000),

-- 14. Trứng Hấp Thịt Bằm
(14, 6, 50.000), (14, 22, 3.000), (14, 23, 1.000), (14, 24, 2.000),
(14, 25, 5.000),

-- 15. Canh Chua Cá Lóc
(15, 9, 200.000), (15, 12, 50.000), (15, 14, 50.000), (15, 17, 20.000),
(15, 22, 5.000), (15, 24, 15.000), (15, 25, 10.000), (15, 27, 500.000),

-- 16. Canh Rau Ngót Thịt Bằm
(16, 6, 50.000), (16, 16, 100.000), (16, 22, 3.000), (16, 25, 5.000),
(16, 27, 500.000),

-- 17. Cơm Chay Thập Cẩm
(17, 1, 200.000), (17, 12, 50.000), (17, 13, 50.000), (17, 29, 2.000),
(17, 21, 15.000), (17, 22, 3.000), (17, 24, 5.000), (17, 25, 10.000),

-- 18. Đậu Hũ Kho Tộ
(18, 29, 3.000), (18, 18, 30.000), (18, 21, 20.000), (18, 22, 2.000),
(18, 24, 10.000), (18, 25, 10.000),

/**********************************************************
 * 19. TRÀ ĐÁ (Đồ uống - bán sẵn)
 * Nguyên liệu bán sẵn: Định lượng 1 chai/ly
 **********************************************************/
(19, 33, 1.000),  -- Trà đá: 1 chai (đơn vị bán)

/**********************************************************
 * 20. NƯỚC MÍA (Đồ uống - bán sẵn)
 * Nguyên liệu bán sẵn: Định lượng 1 chai/ly
 **********************************************************/
(20, 34, 1.000),  -- Nước mía: 1 chai

/**********************************************************
 * 21. NƯỚC SUỐI (Đồ uống đóng chai)
 * Nguyên liệu bán sẵn: Định lượng 1 chai
 **********************************************************/
(21, 35, 1.000),  -- Nước suối: 1 chai

/**********************************************************
 * 22. COCA COLA (Đồ uống đóng chai)
 * Nguyên liệu bán sẵn: Định lượng 1 chai
 **********************************************************/
(22, 36, 1.000),  -- Coca Cola: 1 chai

/**********************************************************
 * 23. PEPSI (Đồ uống đóng chai)
 * Nguyên liệu bán sẵn: Định lượng 1 chai
 **********************************************************/
(23, 37, 1.000),  -- Pepsi: 1 chai

/**********************************************************
 * 24. TRÀ BÍ ĐAO ĐÓNG CHAI (Đồ uống)
 * Nguyên liệu bán sẵn: Định lượng 1 chai
 * Có thêm đường để pha chế
 **********************************************************/
(24, 38, 1.000),  -- Trà bí đao: 1 chai
(24, 24, 10.000), -- Đường: 10g (dùng để pha thêm)

/**********************************************************
 * 25. KEM SỮA (Tráng miệng)
 * Nguyên liệu bán sẵn: Định lượng theo phần
 **********************************************************/
(25, 32, 50.000), -- Sữa đặc: 50ml (dùng để rưới)
(25, 24, 30.000), -- Đường: 30g (dùng thêm)

/**********************************************************
 * 26. BÁNH FLAN (Tráng miệng)
 * Nguyên liệu bán sẵn: Định lượng 1 cái
 * Có thêm nguyên liệu phụ
 **********************************************************/
(26, 30, 1.000),  -- Bánh flan: 1 cái
(26, 32, 30.000), -- Sữa đặc: 30ml (rưới lên)
(26, 24, 20.000), -- Đường: 20g (làm caramel)

/**********************************************************
 * 27. RAU CÂU DỪA (Tráng miệng - chế biến đơn giản)
 * Nguyên liệu chính bán sẵn
 **********************************************************/
(27, 31, 50.000), -- Rau câu: 50g (bột làm thạch)
(27, 28, 50.000), -- Nước dừa: 50ml
(27, 24, 30.000), -- Đường: 30g

/**********************************************************
 * 28. KEM Dừa  (Tráng miệng - chế biến đơn giản)
 * Nguyên liệu chính bán sẵn
 **********************************************************/
(28, 28, 50.000), -- Nước dừa: 50ml
(28, 24, 20.000), -- Đường: 20g
(28, 32, 30.000); -- Sữa đặc: 30ml (rưới lên)


-- 5. Thêm dữ liệu cho bảng Supplier (Nhà cung cấp)
INSERT INTO Supplier (supplierName, phone, email, address) VALUES
(N'Công ty Thực phẩm Sạch', '02838223344', 'info@thucphamsach.vn', N'123 Nguyễn Văn Linh, Q.7, TP.HCM'),
(N'Nông trại Việt', '02839998877', 'contact@nongtraiviet.com', N'456 Lê Văn Việt, Q.9, TP.HCM'),
(N'Hải sản Tươi Sống', '02837776655', 'haisantuoisong@gmail.com', N'789 Võ Văn Kiệt, Q.1, TP.HCM'),
(N'Rau củ Organic', '02836665544', 'raucuorganic@yahoo.com', N'321 Lê Đại Hành, Q.11, TP.HCM'),
(N'Đồ uống Quốc Tế', '02835554433', 'order@douongqt.com', N'159 Pasteur, Q.3, TP.HCM');


-- các triger logic 

-- 1. Tạo Trigger tự động cập nhật tồn kho khi nhập hàng
CREATE TRIGGER trg_AfterImportInsert
ON ImportDetail
AFTER INSERT
AS
BEGIN
    UPDATE i
    SET i.quantity = i.quantity + ins.quantity
    FROM Ingredient i
    INNER JOIN inserted ins ON i.idIngredient = ins.idIngredient
END;


-- 3. Tạo Trigger xử lý khi chỉnh sửa/xóa phiếu nhập
CREATE TRIGGER trg_AfterImportUpdateDelete
ON ImportDetail
AFTER UPDATE, DELETE
AS
BEGIN
    -- Xử lý khi có bản ghi bị xóa (trước khi update)
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE i
        SET i.quantity = i.quantity - d.quantity
        FROM Ingredient i
        JOIN deleted d ON i.idIngredient = d.idIngredient
    END
    
    -- Xử lý khi có bản ghi được thêm (sau khi update)
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE i
        SET i.quantity = i.quantity + ins.quantity
        FROM Ingredient i
        JOIN inserted ins ON i.idIngredient = ins.idIngredient
    END
END;

--4. Tạo Stored Procedure để nhập hàng an toàn
CREATE PROCEDURE sp_ImportIngredient
    @idIngredient INT,
    @quantity DECIMAL(10,3),
    @unitPrice DECIMAL(10,2),
    @importedBy NVARCHAR(100),
    @idSupplier INT
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- 1. Thêm phiếu nhập
        DECLARE @idReceipt INT;
        INSERT INTO ImportReceipt (importedBy, idSupplier) 
        VALUES (@importedBy, @idSupplier);
        SET @idReceipt = SCOPE_IDENTITY();
        
        -- 2. Thêm chi tiết nhập (trigger sẽ tự động cập nhật tồn kho)
        INSERT INTO ImportDetail (idReceipt, idIngredient, quantity, unitPrice)
        VALUES (@idReceipt, @idIngredient, @quantity, @unitPrice);
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

-- 5. Tạo View kiểm tra tồn kho
CREATE VIEW vw_Inventory AS
SELECT 
    i.idIngredient,
    i.ingredientName,
    i.unit,
    i.quantity AS currentQuantity,
    CASE 
        -- Đối với nguyên liệu tính bằng gram/ml (định lượng lớn)
        WHEN i.unit IN ('gram', 'ml') THEN 
            CASE 
                WHEN i.quantity < 1000 THEN N'Sắp hết'
                WHEN i.quantity < 5000 THEN N'Đủ dùng'
                ELSE N'Dồi dào'
            END
        -- Đối với nguyên liệu tính bằng chai/cái/miếng (định lượng nhỏ)
        WHEN i.unit IN ('chai', 'cái', 'miếng') THEN 
            CASE 
                WHEN i.quantity < 50 THEN N'Sắp hết'
                WHEN i.quantity < 200 THEN N'Đủ dùng'
                ELSE N'Dồi dào'
            END
        ELSE N'Không xác định'
    END AS status
FROM Ingredient i;

-- Khi nhập hàng: chạy tốt. 
EXEC sp_ImportIngredient 
    @idIngredient = 1, 
    @quantity = 5000, 
    @unitPrice = 20000, 
    @importedBy = 'staff01', 
    @idSupplier = 1;

-- Xem phiếu nhập vừa tạo
DECLARE @lastReceiptId INT = (SELECT MAX(idReceipt) FROM ImportReceipt);

SELECT * FROM ImportReceipt WHERE idReceipt = @lastReceiptId;
SELECT * FROM ImportDetail WHERE idReceipt = @lastReceiptId;

-- Tạo hóa đơn mới
INSERT INTO Bill (idTable, createdBy) VALUES (2, 'staff01');
DECLARE @billId INT = SCOPE_IDENTITY();

-- Thêm món Cơm Tấm Sườn Bì Chả (idFood = 1) vào hóa đơn
-- Món này sử dụng 200g Gạo tấm theo định lượng
INSERT INTO BillInfo (idBill, idFood, count) VALUES (10, 10, 1);

-- Thanh toán hóa đơn (trigger sẽ tự động trừ kho đã tự động trừ)
UPDATE Bill SET status = 1 WHERE idBill = 1;

-- Kiểm tra tồn kho sau khi bán (oke chạy bthuong)
SELECT idIngredient, ingredientName, quantity 
FROM Ingredient 
WHERE idIngredient = 1;

-- Kiểm tra tồn kho: chạy tốt. 
SELECT * FROM vw_Inventory;

-- tự động cập nhật bằng TRIGER 
CREATE OR ALTER TRIGGER trg_BillManagement
ON Bill
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Ngăn chặn đệ quy
    IF TRIGGER_NESTLEVEL() > 1
        RETURN;
    
    -- Xác định các hóa đơn cần trừ kho (bao gồm cả 2 trường hợp)
    DECLARE @BillsToProcess TABLE (idBill INT);
    
    -- Trường hợp 1: status chuyển từ 0 -> 1
    INSERT INTO @BillsToProcess
    SELECT i.idBill
    FROM inserted i
    JOIN deleted d ON i.idBill = d.idBill
    WHERE i.status = 1 AND d.status = 0;
    
    -- Trường hợp 2: DateCheckOut được set và > DateCheckIn (dù status có thay đổi hay không)
    INSERT INTO @BillsToProcess
    SELECT i.idBill
    FROM inserted i
    WHERE i.DateCheckOut IS NOT NULL 
      AND i.DateCheckOut > i.DateCheckIn
      AND NOT EXISTS (SELECT 1 FROM @BillsToProcess WHERE idBill = i.idBill);
    
    -- Xử lý trừ kho cho tất cả hóa đơn cần xử lý
    IF EXISTS (SELECT 1 FROM @BillsToProcess)
    BEGIN
        -- Trừ kho nguyên liệu
        UPDATE i
        SET i.quantity = i.quantity - (fi.quantity * bi.count)
        FROM Ingredient i
        JOIN FoodIngredient fi ON i.idIngredient = fi.idIngredient
        JOIN BillInfo bi ON fi.idFood = bi.idFood
        JOIN @BillsToProcess b ON bi.idBill = b.idBill;
        
        -- Đồng bộ status và DateCheckOut
        UPDATE bill
        SET 
            status = 1,
            DateCheckOut = CASE 
                              WHEN bill.DateCheckOut IS NULL OR bill.DateCheckOut < bill.DateCheckIn 
                              THEN GETDATE() 
                              ELSE bill.DateCheckOut 
                           END
        FROM Bill bill
        JOIN @BillsToProcess p ON bill.idBill = p.idBill;
    END
END;

-- TRIGER update status bàn theo bill info
CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo
FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @idBill INT

    SELECT @idBill = idBill FROM Inserted

    DECLARE @idTable INT

    SELECT @idTable = idTable FROM dbo.Bill WHERE idBill = @idBill AND status = 0

    UPDATE dbo.TableFood SET status = N'Có người' WHERE idTable = @idTable
END
GO

--  update status bàn theo bill
CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill
FOR UPDATE
AS
BEGIN
    DECLARE @idBill INT;
    SELECT @idBill = idBill FROM Inserted;

    DECLARE @idTable INT;
    SELECT @idTable = idTable FROM dbo.Bill WHERE idBill = @idBill;

    DECLARE @count INT = 0;
    SELECT @count = COUNT(*) 
    FROM dbo.Bill 
    WHERE idTable = @idTable AND status = 0;

    IF (@count = 0)
        UPDATE dbo.TableFood 
        SET status = N'Trống' 
        WHERE idTable = @idTable;
END
GO


-- ktra 
-- Test 1: Tạo hóa đơn mới
INSERT INTO Bill (idTable, createdBy, DateCheckIn) 
VALUES (1, 'staff01', GETDATE());

-- Thêm món ăn
INSERT INTO BillInfo (idBill, idFood, count) VALUES (8, 1, 1);
SELECT * FROM vw_Inventory;

-- Test 2: Thanh toán bằng cách đổi status (đã oke tự set date và tự trừ nlieeu kho)
UPDATE Bill SET status = 1 WHERE idBill = 8;
SELECT * FROM Bill;

-- Test 3: Tạo hóa đơn khác
INSERT INTO Bill (idTable, createdBy, DateCheckIn) 
VALUES (1, 'staff01', GETDATE());
INSERT INTO BillInfo (idBill, idFood, count) VALUES (9, 2, 2);
SELECT * FROM BillInfo;

-- Thanh toán bằng cách set DateCheckOut (oke tự set status và nguyên liệu kho đã giảm)
UPDATE Bill SET DateCheckOut = GETDATE() WHERE idBill = 9;
SELECT * FROM Bill;

-- Kiểm tra kho
SELECT * FROM vw_Inventory;



-- tạo các process
CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100) 
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO 

EXEC dbo.USP_GetAccountByUserName @userName = "admin"
GO

CREATE OR ALTER PROC USP_GetAccount
AS
BEGIN
    SELECT 
        UserName AS N'Tài khoản',
        CASE 
            WHEN Type = 1 THEN N'Admin'
            ELSE N'Nhân viên'
        END AS N'Loại tài khoản',
        CASE 
            WHEN isActive = 1 THEN N'Khả dụng'
            ELSE N'Bị khóa'
        END AS N'Tình trạng'
    FROM 
        dbo.Account;
END

EXEC  USP_GetAccount 

GO 
-- proc Login
CREATE PROC USP_Login
@userName nvarchar(100), 
@passWord nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO 

-- login thêm kiểm tra isactive 
CREATE OR ALTER PROCEDURE USP_Login
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra tài khoản và mật khẩu đúng không
    IF NOT EXISTS (
        SELECT 1 FROM Account WHERE UserName = @UserName AND Password = @Password
    )
    BEGIN
        SELECT -1 AS ResultCode -- Sai tài khoản hoặc mật khẩu
        RETURN
    END

    -- 2. Kiểm tra tài khoản có bị khóa không
    IF EXISTS (
        SELECT 1 FROM Account WHERE UserName = @UserName AND Password = @Password AND isActive = 0
    )
    BEGIN
        SELECT 0 AS ResultCode -- Tài khoản bị khóa
        RETURN
    END

    -- 3. Thành công
    SELECT 1 AS ResultCode -- Đăng nhập thành công
END


-- PRO display table
CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO


-- 
CREATE PROCEDURE GetThuChi_ByDate
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Doanh thu: hóa đơn đã thanh toán trong khoảng ngày
    SELECT 
        B.idBill,
        B.DateCheckOut,
        B.idTable,
        B.createdBy,
        F.foodName,
        BI.count,
        F.price,
        (BI.count * F.price) AS totalPrice
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE 
        B.status = 1
        AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    ORDER BY B.DateCheckOut DESC;

    -- Tổng doanh thu
    SELECT 
        SUM(BI.count * F.price) AS TotalThu
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE 
        B.status = 1
        AND B.DateCheckOut BETWEEN @FromDate AND @ToDate;

    -- Chi phí: nhập nguyên liệu trong khoảng ngày
    SELECT 
        IR.idReceipt,
        IR.importDate,
        IR.importedBy,
        S.supplierName,
        IDT.idIngredient,
        I.ingredientName,
        IDT.quantity,
        IDT.unitPrice,
        (IDT.quantity * IDT.unitPrice) AS totalCost
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN Ingredient I ON IDT.idIngredient = I.idIngredient
    LEFT JOIN Supplier S ON IR.idSupplier = S.idSupplier
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate
    ORDER BY IR.importDate DESC;

    -- Tổng chi phí
    SELECT 
        SUM(IDT.quantity * IDT.unitPrice) AS TotalChi
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate;
END


--ktra 
EXEC GetThuChi_ByDate '2025-06-01', '2025-06-20';


--
CREATE PROCEDURE GetThuChi_Lai_ByDate
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Tổng thu
    DECLARE @TongThu DECIMAL(18,2) = (
        SELECT 
            ISNULL(SUM(BI.count * F.price), 0)
        FROM 
            Bill B
        INNER JOIN BillInfo BI ON B.idBill = BI.idBill
        INNER JOIN Food F ON BI.idFood = F.idFood
        WHERE 
            B.status = 1
            AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    );

    -- Tổng chi
    DECLARE @TongChi DECIMAL(18,2) = (
        SELECT 
            ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM 
            ImportReceipt IR
        INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
        WHERE 
            IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Trả về kết quả
    SELECT 
        @TongThu AS TongThu,
        @TongChi AS TongChi,
        (@TongThu - @TongChi) AS TongLai;
END

-- ktra 
EXEC GetThuChi_Lai_ByDate '2025-06-01', '2025-06-20';


--
CREATE PROCEDURE GetThuChi_OneTable_WithSummary
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Phần chi tiết: Doanh thu + Chi phí
    SELECT 
        B.DateCheckOut AS Ngay,
        N'Doanh thu' AS Loai,
        F.foodName AS TenHang,
        BI.count AS SoLuong,
        F.price AS DonGia,
        (BI.count * F.price) AS ThanhTien
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE 
        B.status = 1
        AND B.DateCheckOut BETWEEN @FromDate AND @ToDate

    UNION ALL

    SELECT 
        IR.importDate AS Ngay,
        N'Chi phí',
        I.ingredientName,
        IDT.quantity,
        IDT.unitPrice,
        (IDT.quantity * IDT.unitPrice)
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN Ingredient I ON IDT.idIngredient = I.idIngredient
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Phần tổng kết: chỉ 1 dòng tổng
    SELECT
        NULL AS Ngay,
        N'Tổng kết' AS Loai,
        NULL AS TenHang,
        NULL AS SoLuong,
        NULL AS DonGia,
        (TongThu - TongChi) AS ThanhTien
    FROM (
        SELECT
            ISNULL((
                SELECT SUM(BI.count * F.price)
                FROM Bill B
                INNER JOIN BillInfo BI ON B.idBill = BI.idBill
                INNER JOIN Food F ON BI.idFood = F.idFood
                WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
            ), 0) AS TongThu,
            ISNULL((
                SELECT SUM(IDT.quantity * IDT.unitPrice)
                FROM ImportReceipt IR
                INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
                WHERE IR.importDate BETWEEN @FromDate AND @ToDate
            ), 0) AS TongChi
    ) AS X
    ORDER BY 
        Ngay DESC OFFSET 0 ROWS;
END


CREATE OR ALTER PROCEDURE GetThuChi_OneTable_WithNote
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính tổng thu
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(BI.count * F.price), 0)
        FROM Bill B
        INNER JOIN BillInfo BI ON B.idBill = BI.idBill
        INNER JOIN Food F ON BI.idFood = F.idFood
        WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    );

    -- Tính tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR
        INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Chi tiết Doanh thu có ghi chú người tạo
    SELECT 
        B.DateCheckOut AS [Ngày],
        N'Doanh thu' AS [Loại],
        F.foodName AS [Tên Hàng],
        CEILING(BI.count) AS [Số Lượng],
        CEILING(F.price) AS [Đơn Giá],
        CEILING(BI.count * F.price) AS [Thành Tiền],
        N'Tạo bởi: ' + S.fullName + ' (' + S.accountUserName + ')' AS [Ghi Chú]
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    INNER JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE 
        B.status = 1
        AND B.DateCheckOut BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Chi tiết Chi phí
    SELECT 
        IR.importDate AS [Ngày],
        N'Chi phí' AS [Loại],
        I.ingredientName AS [Tên Hàng],
        CEILING(IDT.quantity) AS [Số Lượng],
        CEILING(IDT.unitPrice) AS [Đơn Giá],
        CEILING(IDT.quantity * IDT.unitPrice) AS [Thành Tiền],
        NULL AS [Ghi Chú]
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN Ingredient I ON IDT.idIngredient = I.idIngredient
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Tổng kết: Tổng thu, tổng chi, lợi nhuận
    SELECT 
        NULL AS [Ngày],
        N'Tổng kết' AS [Loại],
        NULL AS [Tên Hàng],
        NULL AS [Số Lượng],
        NULL AS [Đơn Giá],
        NULL AS [Thành Tiền],
        N'Tổng thu: ' + FORMAT(@TongThu, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Tổng chi: ' + FORMAT(@TongChi, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Lợi nhuận: ' + FORMAT(@TongThu - @TongChi, 'N0')

    ORDER BY [Ngày] DESC, [Loại];
END


EXEC GetThuChi_OneTable_WithSummary '2025-06-01', '2025-06-20';


CREATE PROCEDURE GetThuChi_OneTable_WithNote
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Tính tổng thu
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(BI.count * F.price), 0)
        FROM Bill B
        INNER JOIN BillInfo BI ON B.idBill = BI.idBill
        INNER JOIN Food F ON BI.idFood = F.idFood
        WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    );

    -- Tính tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR
        INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Chi tiết Doanh thu
    SELECT 
        B.DateCheckOut AS [Ngày],
        N'Doanh thu' AS [Loại],
        F.foodName AS [Tên Hàng],
        CEILING(BI.count) AS [Số Lượng],
        CEILING(F.price) AS [Đơn Giá],
        CEILING(BI.count * F.price) AS [Thành Tiền],
        NULL AS [Ghi Chú]
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE 
        B.status = 1
        AND B.DateCheckOut BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Chi tiết Chi phí
    SELECT 
        IR.importDate AS [Ngày],
        N'Chi phí' AS [Loại],
        I.ingredientName AS [Tên Hàng],
        CEILING(IDT.quantity) AS [Số Lượng],
        CEILING(IDT.unitPrice) AS [Đơn Giá],
        CEILING(IDT.quantity * IDT.unitPrice) AS [Thành Tiền],
        NULL AS [Ghi Chú]
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN Ingredient I ON IDT.idIngredient = I.idIngredient
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Dòng ghi chú Tổng thu
    SELECT 
        NULL AS [Ngày],
        N'Tổng kết' AS [Loại],
        NULL AS [Tên Hàng],
        NULL AS [Số Lượng],
        NULL AS [Đơn Giá],
        NULL AS [Thành Tiền],
        N'Tổng thu: ' + FORMAT(@TongThu, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Tổng chi: ' + FORMAT(@TongChi, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Lợi nhuận: ' + FORMAT(@TongThu - @TongChi, 'N0')

    ORDER BY [Ngày] DESC, [Loại];
END


EXEC GetThuChi_OneTable_WithNote '2025-06-01', '2025-06-20';


-- Thống kê nhập hàng.
CREATE OR ALTER PROCEDURE dbo.GetThongKeNhapHang
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IR.idReceipt AS [Mã Phiếu Nhập],
        IR.importDate AS [Ngày Nhập],
        S.fullName + ' (' + S.accountUserName + ')' AS [Nhân Viên Nhập],
        SP.supplierName + ' (ID: ' + CAST(SP.idSupplier AS NVARCHAR) + ')' AS [Nhà Cung Cấp],
        I.ingredientName AS [Nguyên Liệu],
        IDT.quantity AS [Số Lượng],
        I.unit AS [Đơn Vị],
        IDT.unitPrice AS [Đơn Giá],
        IDT.quantity * IDT.unitPrice AS [Thành Tiền]
    FROM 
        dbo.ImportReceipt IR
    INNER JOIN dbo.ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN dbo.Ingredient I ON IDT.idIngredient = I.idIngredient
    INNER JOIN dbo.Supplier SP ON IR.idSupplier = SP.idSupplier
    INNER JOIN dbo.Staff S ON IR.importedBy = S.accountUserName
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate
    ORDER BY 
        IR.importDate DESC, IR.idReceipt;
END



EXEC GetThongKeNhapHang  @FromDate = '2025-06-19',  @ToDate = '2025-06-20';

-- dtgv định lượng
CREATE PROCEDURE GetFoodIngredientSummary
AS
BEGIN
    SELECT 
        F.foodName AS N'Tên món',
        STRING_AGG(
            I.ingredientName + N' (' +
            -- Hiển thị số nếu là nguyên: không thập phân; nếu là số lẻ: giữ thập phân
            CASE 
                WHEN FI.quantity = FLOOR(FI.quantity) 
                THEN FORMAT(FI.quantity, 'N0') 
                ELSE FORMAT(FI.quantity, 'N3')
            END
            + N' ' + I.unit + N')',
            N', '
        ) AS N'Định lượng'
    FROM 
        FoodIngredient FI
    INNER JOIN Food F ON FI.idFood = F.idFood
    INNER JOIN Ingredient I ON FI.idIngredient = I.idIngredient
    GROUP BY 
        F.foodName
    ORDER BY 
        F.foodName;
END


EXEC GetFoodIngredientSummary

-- dtgv staff

CREATE OR ALTER PROCEDURE USP_GetStaff
AS
BEGIN
    SELECT 
        idStaff AS [Mã Nhân Viên],
        fullName AS [Họ Và Tên],
        gender AS [Giới Tính],
        birthDate AS [Ngày Sinh],
        phone AS [Số Điện Thoại],
        email AS [Email],
        accountUserName AS [Tên Đăng Nhập]
    FROM 
        Staff
    ORDER BY 
        idStaff;
END

EXEC USP_GetStaff


-- 
CREATE OR ALTER PROCEDURE USP_GetTableFood
AS
BEGIN
    SELECT 
        idTable AS [Mã Bàn],
        tableName AS [Tên Bàn],
        status AS [Trạng Thái]
    FROM 
        TableFood
    ORDER BY 
        idTable;
END

EXEC USP_GetTableFood

-- 

CREATE OR ALTER PROCEDURE USP_GetFoodCategory
AS
BEGIN
    SELECT 
        idCategory AS [Mã Danh Mục],
        categoryName AS [Tên Danh Mục]
    FROM 
        FoodCategory
    ORDER BY 
        categoryName;
END

EXEC USP_GetFoodCategory

-- 
CREATE OR ALTER PROCEDURE USP_GetIngredient
AS
BEGIN
    SELECT 
        idIngredient AS [Mã Nguyên Liệu],
        ingredientName AS [Tên Nguyên Liệu],
        unit AS [Đơn Vị],
        -- Làm tròn lên, bỏ phần thập phân nếu không cần
        CASE 
            WHEN quantity = FLOOR(quantity) THEN FORMAT(quantity, 'N0')
            ELSE FORMAT(quantity, 'N3')
        END AS [Số Lượng]
    FROM 
        Ingredient
    ORDER BY 
        ingredientName;
END


EXEC USP_GetIngredient

--
CREATE OR ALTER PROCEDURE USP_GetSupplier
AS
BEGIN
    SELECT 
        idSupplier AS [Mã Nhà Cung Cấp],
        supplierName AS [Tên Nhà Cung Cấp],
        phone AS [Số Điện Thoại],
        email AS [Email],
        address AS [Địa Chỉ]
    FROM 
        Supplier
    ORDER BY 
        supplierName;
END


EXEC USP_GetSupplier

select * from dbo.Bill
select * from dbo.BillInfo
INSERT INTO Bill (idTable, createdBy) VALUES (2, 'staff01');
DECLARE @billId INT = SCOPE_IDENTITY();

-- Thêm món Cơm Tấm Sườn Bì Chả (idFood = 1) vào hóa đơn
INSERT INTO BillInfo (idBill, idFood, count) VALUES (10, 10, 1);


-- Proceture tạo Bill
Create PROCEDURE USP_InsertBill
    @idTable INT,
    @createdBy NVARCHAR(100)
AS
BEGIN
    INSERT dbo.Bill
        (DateCheckIn, DateCheckOut, idTable, status, createdBy)
    VALUES
        (GETDATE(), NULL, @idTable, 0, @createdBy)
END
GO

-- Insert BILL Info
Create Procedure USP_InsertBillInfo 
@idBill INT, @idFood INT, @count INT 
AS
BEGIN 
	INSERT	dbo.BillInfo
			(idBill, idFood, count)
			Values
			(@idBill, @idFood, @count)
END

-- sửa lại
ALTER PROC USP_InsertBillInfo
    @idBill INT,
    @idFood INT,
    @count INT 
AS
BEGIN
    DECLARE @foodCount INT = 0;

    SELECT @foodCount = count 
    FROM dbo.BillInfo 
    WHERE idBill = @idBill AND idFood = @idFood;

    IF (@foodCount > 0)
    BEGIN 
        DECLARE @newCount INT = @foodCount + @count;

        IF (@newCount > 0)
        BEGIN
            UPDATE dbo.BillInfo 
            SET count = @newCount 
            WHERE idBill = @idBill AND idFood = @idFood;
        END
        ELSE 
        BEGIN
            DELETE FROM dbo.BillInfo 
            WHERE idBill = @idBill AND idFood = @idFood;
        END
    END
    ELSE 
    BEGIN
        INSERT INTO dbo.BillInfo (idBill, idFood, count)
        VALUES (@idBill, @idFood, @count);
    END
END

-- bản insert billInfo mới không lỗi thêm món âm 
ALTER PROCEDURE USP_InsertBillInfo
    @idBill INT,
    @idFood INT,
    @count INT 
AS
BEGIN
    SET NOCOUNT ON;

    -- Nếu số lượng nhập là 0 thì không làm gì cả
    IF @count = 0
        RETURN;

    DECLARE @currentCount INT;

    SELECT @currentCount = count 
    FROM dbo.BillInfo 
    WHERE idBill = @idBill AND idFood = @idFood;

    IF @currentCount IS NOT NULL
    BEGIN 
        DECLARE @newCount INT = @currentCount + @count;

        IF @newCount > 0
        BEGIN
            UPDATE dbo.BillInfo 
            SET count = @newCount 
            WHERE idBill = @idBill AND idFood = @idFood;
        END
        ELSE
        BEGIN
            -- Nếu tổng số lượng <= 0 thì huỷ món khỏi hóa đơn
            DELETE FROM dbo.BillInfo 
            WHERE idBill = @idBill AND idFood = @idFood;
        END
    END
    ELSE
    BEGIN
        -- Nếu món chưa từng có, chỉ thêm khi số lượng dương
        IF @count > 0
        BEGIN
            INSERT INTO dbo.BillInfo (idBill, idFood, count)
            VALUES (@idBill, @idFood, @count);
        END
        -- Nếu nhân viên cố thêm món âm khi chưa từng có thì bỏ qua
    END
END

SELECT * FROM TableFood WHERE tableName = N'Mang Về'

-- proc update pass account
CREATE PROCEDURE USP_UpdatePassword
    @userName NVARCHAR(100),
    @oldPassword NVARCHAR(100),
    @newPassword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.Account
        WHERE UserName = @userName AND Password = @oldPassword
    )
    BEGIN
        SELECT -1 AS ResultCode -- Sai mật khẩu hiện tại
        RETURN
    END

    IF EXISTS (
        SELECT 1 FROM dbo.Account
        WHERE UserName = @userName AND Password = @oldPassword AND isActive = 0
    )
    BEGIN
        SELECT 0 AS ResultCode -- Tài khoản bị vô hiệu hóa
        RETURN
    END

    -- Đổi mật khẩu
    UPDATE dbo.Account
    SET Password = @newPassword
    WHERE UserName = @userName;

    SELECT 1 AS ResultCode -- Thành công
END
GO

-- update thông tin nhân viên
CREATE PROC USP_UpdateStaff
    @fullName NVARCHAR(100),
    @gender NVARCHAR(10),
    @birthDate DATE,
    @phone VARCHAR(20),
    @email NVARCHAR(100),
    @accountUserName NVARCHAR(100)
AS
BEGIN
    UPDATE Staff
    SET
        fullName = @fullName,
        gender = @gender,
        birthDate = @birthDate,
        phone = @phone,
        email = @email
    WHERE accountUserName = @accountUserName
END


use quanlyquanan


-- proc mới về doanh thu 
CREATE OR ALTER PROCEDURE DoanhThu 
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Tính tổng thu
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(BI.count * F.price), 0)
        FROM Bill B
        INNER JOIN BillInfo BI ON B.idBill = BI.idBill
        INNER JOIN Food F ON BI.idFood = F.idFood
        WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    );

    -- Tính tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR
        INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Chi tiết Doanh thu có ghi chú (tên + mã + tài khoản nhân viên)
    SELECT 
        B.DateCheckOut AS [Ngày],
        N'Doanh thu' AS [Loại],
        F.foodName AS [Tên Hàng],
        CEILING(BI.count) AS [Số Lượng],
        CEILING(F.price) AS [Đơn Giá],
        CEILING(BI.count * F.price) AS [Thành Tiền],
        ISNULL(
            N'Nhân viên: ' + S.fullName + N' - Mã nhân viên: ' + CAST(S.idStaff AS NVARCHAR) + N' - Tài khoản: ' + S.accountUserName,
            N'Tài khoản: ' + B.createdBy
        ) AS [Tạo Bởi]
    FROM 
        Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE 
        B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Chi tiết Chi phí có ghi chú (tên + mã + tài khoản nhân viên)
    SELECT 
        IR.importDate AS [Ngày],
        N'Chi phí' AS [Loại],
        I.ingredientName AS [Tên Hàng],
        CEILING(IDT.quantity) AS [Số Lượng],
        CEILING(IDT.unitPrice) AS [Đơn Giá],
        CEILING(IDT.quantity * IDT.unitPrice) AS [Thành Tiền],
        ISNULL(
            N'Nhân viên: ' + S.fullName + N' - Mã nhân viên: ' + CAST(S.idStaff AS NVARCHAR) + N' - Tài khoản: ' + S.accountUserName,
            N'Tài khoản: ' + IR.importedBy
        ) AS [Tạo Bởi]
    FROM 
        ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    INNER JOIN Ingredient I ON IDT.idIngredient = I.idIngredient
    LEFT JOIN Staff S ON IR.importedBy = S.accountUserName
    WHERE 
        IR.importDate BETWEEN @FromDate AND @ToDate

    UNION ALL

    -- Ghi chú tổng kết
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Tổng thu: ' + FORMAT(@TongThu, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Tổng chi: ' + FORMAT(@TongChi, 'N0')
    UNION ALL
    SELECT 
        NULL, N'Tổng kết', NULL, NULL, NULL, NULL,
        N'Lợi nhuận: ' + FORMAT(@TongThu - @TongChi, 'N0')

    ORDER BY [Ngày] DESC, [Loại];
END


use quanlyquanan
CREATE OR ALTER PROCEDURE DoanhThu
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    -- Tổng thu (cộng phụ phí VIP nếu có)
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(TotalAmount), 0)
        FROM (
            SELECT 
                B.idBill,
                SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END AS TotalAmount
            FROM Bill B
            INNER JOIN TableFood T ON B.idTable = T.idTable
            INNER JOIN BillInfo BI ON B.idBill = BI.idBill
            INNER JOIN Food F ON BI.idFood = F.idFood
            WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
            GROUP BY B.idBill, T.tableName
        ) AS Temp
    );

    -- Tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR
        INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Xuất danh sách doanh thu (mỗi hóa đơn 1 dòng)
    SELECT 
        B.idBill AS [Mã Hóa Đơn],
        B.DateCheckOut AS [Ngày],
        T.tableName AS [Bàn],
        SUM(BI.count) AS [Tổng Số Món],
        SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END AS [Tổng Tiền],
        ISNULL(S.fullName, B.createdBy) AS [Nhân Viên]
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    GROUP BY B.idBill, B.DateCheckOut, T.tableName, S.fullName, B.createdBy
    ORDER BY B.DateCheckOut DESC;

    -- Tổng kết
    SELECT 
        FORMAT(@TongThu, 'N0') AS [Tổng Thu],
        FORMAT(@TongChi, 'N0') AS [Tổng Chi],
        FORMAT(@TongThu - @TongChi, 'N0') AS [Lợi Nhuận];
END

EXEC DoanhThu '2025-07-28', '2025-07-30';


CREATE OR ALTER PROCEDURE ThongKeBill
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SELECT 
        B.idBill AS [Mã HĐ],
        T.tableName AS [Bàn],
        F.foodName AS [Tên Món],
        SUM(BI.count) AS [Số Lượng],
        F.price AS [Đơn Giá],
        SUM(BI.count * F.price) AS [Thành Tiền]
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    GROUP BY B.idBill, T.tableName, F.foodName, F.price
    ORDER BY B.idBill;
END

EXEC ThongKeBill '2025-07-28', '2025-07-30';

CREATE OR ALTER PROCEDURE MonAnBanChay
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SELECT 
        F.foodName AS [Món Ăn],
        SUM(BI.count) AS [Số Lượng Bán]
    FROM Bill B
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    GROUP BY F.foodName
    ORDER BY SUM(BI.count) DESC; -- Sắp xếp theo số lượng bán
END

EXEC MonAnBanChay '2025-07-28', '2025-07-30';


CREATE OR ALTER PROCEDURE GetBillDetails
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo số dòng ảnh hưởng để tăng hiệu suất

    DECLARE @TableName NVARCHAR(100);
    DECLARE @BillCode NVARCHAR(50);
    DECLARE @ExtraFee INT = 0;
    DECLARE @Total DECIMAL(18,0) = 0;

    -- Lấy mã hóa đơn và tên bàn
    SELECT @BillCode = ISNULL(B.idBill, N'Không có mã'), @TableName = T.tableName
    FROM dbo.Bill B WITH (NOLOCK)
    INNER JOIN dbo.TableFood T WITH (NOLOCK) ON B.idTable = T.idTable
    WHERE B.idBill = @BillID;

    -- Nếu bàn VIP thì phụ thu 20000
    IF @TableName IS NOT NULL AND LOWER(@TableName) LIKE '%vip%'
        SET @ExtraFee = 20000;

    -- Tính tổng tiền món
    SELECT @Total = ISNULL(SUM(BI.count * F.price), 0)
    FROM dbo.BillInfo BI WITH (NOLOCK)
    INNER JOIN dbo.Food F WITH (NOLOCK) ON BI.idFood = F.idFood
    WHERE BI.idBill = @BillID;

    SET @Total = @Total + @ExtraFee;

    -- Trả về kết quả
    -- Danh sách món ăn
    SELECT 
        CASE WHEN ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) = 1 THEN @BillCode ELSE NULL END AS [Mã Hóa Đơn],
        CASE WHEN ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) = 1 THEN @TableName ELSE NULL END AS [Tên Bàn],
        F.foodName AS [Tên Món],
        CAST(BI.count AS NVARCHAR) AS [Số Lượng],
        FORMAT(F.price, 'N0') AS [Đơn Giá],
        FORMAT(BI.count * F.price, 'N0') AS [Thành Tiền]
    FROM dbo.BillInfo BI WITH (NOLOCK)
    INNER JOIN dbo.Food F WITH (NOLOCK) ON BI.idFood = F.idFood
    WHERE BI.idBill = @BillID

    UNION ALL

    -- Dòng phụ thu (nếu có)
    SELECT 
        CASE WHEN NOT EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @BillID) THEN @BillCode ELSE NULL END AS [Mã Hóa Đơn],
        CASE WHEN NOT EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @BillID) THEN @TableName ELSE NULL END AS [Tên Bàn],
        N'Phụ thu bàn VIP' AS [Tên Món],
        CAST(1 AS NVARCHAR) AS [Số Lượng],
        FORMAT(20000, 'N0') AS [Đơn Giá],
        FORMAT(@ExtraFee, 'N0') AS [Thành Tiền]
    WHERE @ExtraFee > 0

    UNION ALL

    -- Dòng tổng cộng
    SELECT 
        CASE WHEN NOT EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @BillID) AND @ExtraFee = 0 THEN @BillCode ELSE NULL END AS [Mã Hóa Đơn],
        CASE WHEN NOT EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @BillID) AND @ExtraFee = 0 THEN @TableName ELSE NULL END AS [Tên Bàn],
        N'Tổng cộng' AS [Tên Món],
        NULL AS [Số Lượng],
        NULL AS [Đơn Giá],
        FORMAT(@Total, 'N0') AS [Thành Tiền];
END
GO


EXEC GetBillDetails 8;


CREATE OR ALTER PROCEDURE DoanhThu
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo số dòng ảnh hưởng để tăng hiệu suất

    -- Tổng thu (cộng phụ phí VIP nếu có)
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(TotalAmount), 0)
        FROM (
            SELECT 
                B.idBill,
                SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END AS TotalAmount
            FROM Bill B WITH (NOLOCK)
            INNER JOIN TableFood T WITH (NOLOCK) ON B.idTable = T.idTable
            INNER JOIN BillInfo BI WITH (NOLOCK) ON B.idBill = BI.idBill
            INNER JOIN Food F WITH (NOLOCK) ON BI.idFood = F.idFood
            WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
            GROUP BY B.idBill, T.tableName
        ) AS Temp
    );

    -- Tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR WITH (NOLOCK)
        INNER JOIN ImportDetail IDT WITH (NOLOCK) ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Xuất danh sách doanh thu và chi phí
    SELECT 
        N'Thu' AS [Loại],
        CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
        B.DateCheckOut AS [Ngày],
        T.tableName AS [Bàn/NCC],
        SUM(BI.count) AS [Tổng SL],
        FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, B.createdBy) AS [Nhân Viên],
        1 AS SortOrder -- Thu: 1
    FROM Bill B WITH (NOLOCK)
    INNER JOIN TableFood T WITH (NOLOCK) ON B.idTable = T.idTable
    INNER JOIN BillInfo BI WITH (NOLOCK) ON B.idBill = BI.idBill
    INNER JOIN Food F WITH (NOLOCK) ON BI.idFood = F.idFood
    LEFT JOIN Staff S WITH (NOLOCK) ON B.createdBy = S.accountUserName
    WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    GROUP BY B.idBill, B.DateCheckOut, T.tableName, S.fullName, B.createdBy

    UNION ALL

    -- Phần chi (phiếu nhập)
    SELECT 
        N'Chi' AS [Loại],
        CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
        IR.importDate AS [Ngày],
        ISNULL(SU.supplierName, N'Không xác định') AS [Bàn/NCC],
        SUM(IDT.quantity) AS [Tổng SL],
        FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, IR.importedBy) AS [Nhân Viên],
        2 AS SortOrder -- Chi: 2
    FROM ImportReceipt IR WITH (NOLOCK)
    INNER JOIN ImportDetail IDT WITH (NOLOCK) ON IR.idReceipt = IDT.idReceipt
    LEFT JOIN Supplier SU WITH (NOLOCK) ON IR.idSupplier = SU.idSupplier
    LEFT JOIN Staff S WITH (NOLOCK) ON IR.importedBy = S.accountUserName
    WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    GROUP BY IR.idReceipt, IR.importDate, SU.supplierName, S.fullName, IR.importedBy

    UNION ALL

    -- Dòng Tổng Thu
    SELECT 
        N'Tổng kết' AS [Loại],
        N'Tổng Thu' AS [Mã HĐ/PN],
        NULL AS [Ngày],
        NULL AS [Bàn/NCC],
        NULL AS [Tổng SL],
        FORMAT(@TongThu, 'N0') + N' VND' AS [Tổng Tiền],
        NULL AS [Nhân Viên],
        3 AS SortOrder -- Tổng kết: 3
    WHERE @TongThu IS NOT NULL

    UNION ALL

    -- Dòng Tổng Chi
    SELECT 
        N'Tổng kết' AS [Loại],
        N'Tổng Chi' AS [Mã HĐ/PN],
        NULL AS [Ngày],
        NULL AS [Bàn/NCC],
        NULL AS [Tổng SL],
        FORMAT(@TongChi, 'N0') + N' VND' AS [Tổng Tiền],
        NULL AS [Nhân Viên],
        3 AS SortOrder -- Tổng kết: 3
    WHERE @TongChi IS NOT NULL

    UNION ALL

    -- Dòng Lợi Nhuận
    SELECT 
        N'Tổng kết' AS [Loại],
        N'Lợi Nhuận' AS [Mã HĐ/PN],
        NULL AS [Ngày],
        NULL AS [Bàn/NCC],
        NULL AS [Tổng SL],
        FORMAT(@TongThu - @TongChi, 'N0') + N' VND' AS [Tổng Tiền],
        NULL AS [Nhân Viên],
        3 AS SortOrder -- Tổng kết: 3
    WHERE @TongThu IS NOT NULL OR @TongChi IS NOT NULL

    ORDER BY SortOrder, [Ngày] DESC, [Mã HĐ/PN];
END
GO


CREATE OR ALTER PROCEDURE GetImportDetails
    @ReceiptID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ReceiptCode NVARCHAR(50);
    DECLARE @ImportDate DATETIME;
    DECLARE @Total DECIMAL(18,0) = 0;

    -- Lấy mã phiếu nhập và ngày nhập
    SELECT 
        @ReceiptCode = CAST(idReceipt AS NVARCHAR),
        @ImportDate = importDate
    FROM ImportReceipt WITH (NOLOCK)
    WHERE idReceipt = @ReceiptID;

    -- Tính tổng tiền
    SELECT @Total = ISNULL(SUM(quantity * unitPrice), 0)
    FROM ImportDetail WITH (NOLOCK)
    WHERE idReceipt = @ReceiptID;

    -- Danh sách chi tiết phiếu nhập
    SELECT
        CASE WHEN ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) = 1 THEN @ReceiptCode ELSE NULL END AS [Mã Phiếu Nhập],
        CASE WHEN ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) = 1 THEN FORMAT(@ImportDate, 'dd/MM/yyyy HH:mm') ELSE NULL END AS [Ngày Nhập],
        ING.ingredientName AS [Nguyên Liệu],
        CAST(IDT.quantity AS NVARCHAR) + N' ' + ING.unit AS [Số Lượng],
        FORMAT(IDT.unitPrice, 'N0') AS [Đơn Giá],
        FORMAT(IDT.quantity * IDT.unitPrice, 'N0') AS [Thành Tiền]
    FROM ImportDetail IDT WITH (NOLOCK)
    INNER JOIN Ingredient ING WITH (NOLOCK) ON IDT.idIngredient = ING.idIngredient
    WHERE IDT.idReceipt = @ReceiptID

    UNION ALL

    -- Dòng tổng cộng
    SELECT 
        NULL AS [Mã Phiếu Nhập],
        NULL AS [Ngày Nhập],
        N'Tổng cộng' AS [Nguyên Liệu],
        NULL AS [Số Lượng],
        NULL AS [Đơn Giá],
        FORMAT(@Total, 'N0') AS [Thành Tiền];
END
GO



CREATE OR ALTER PROCEDURE DoanhThu
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON; -- Tắt thông báo số dòng ảnh hưởng để tăng hiệu suất

    -- Tổng thu (cộng phụ phí VIP nếu có)
    DECLARE @TongThu DECIMAL(18,0) = (
        SELECT ISNULL(SUM(TotalAmount), 0)
        FROM (
            SELECT 
                B.idBill,
                SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END AS TotalAmount
            FROM Bill B WITH (NOLOCK)
            INNER JOIN TableFood T WITH (NOLOCK) ON B.idTable = T.idTable
            INNER JOIN BillInfo BI WITH (NOLOCK) ON B.idBill = BI.idBill
            INNER JOIN Food F WITH (NOLOCK) ON BI.idFood = F.idFood
            WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
            GROUP BY B.idBill, T.tableName
        ) AS Temp
    );

    -- Tổng chi
    DECLARE @TongChi DECIMAL(18,0) = (
        SELECT ISNULL(SUM(IDT.quantity * IDT.unitPrice), 0)
        FROM ImportReceipt IR WITH (NOLOCK)
        INNER JOIN ImportDetail IDT WITH (NOLOCK) ON IR.idReceipt = IDT.idReceipt
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    );

    -- Sử dụng CTE để gộp kết quả và sắp xếp
    WITH CombinedResults AS (
        -- Phần thu (hóa đơn)
        SELECT 
            N'Thu' AS [Loại],
            CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
            B.DateCheckOut AS [Ngày],
            T.tableName AS [Bàn/NCC],
            SUM(BI.count) AS [Tổng SL],
            FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
            ISNULL(S.fullName, B.createdBy) AS [Nhân Viên]
        FROM Bill B WITH (NOLOCK)
        INNER JOIN TableFood T WITH (NOLOCK) ON B.idTable = T.idTable
        INNER JOIN BillInfo BI WITH (NOLOCK) ON B.idBill = BI.idBill
        INNER JOIN Food F WITH (NOLOCK) ON BI.idFood = F.idFood
        LEFT JOIN Staff S WITH (NOLOCK) ON B.createdBy = S.accountUserName
        WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
        GROUP BY B.idBill, B.DateCheckOut, T.tableName, S.fullName, B.createdBy

        UNION ALL

        -- Phần chi (phiếu nhập)
        SELECT 
            N'Chi' AS [Loại],
            CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
            IR.importDate AS [Ngày],
            ISNULL(SU.supplierName, N'Không xác định') AS [Bàn/NCC],
            SUM(IDT.quantity) AS [Tổng SL],
            FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
            ISNULL(S.fullName, IR.importedBy) AS [Nhân Viên]
        FROM ImportReceipt IR WITH (NOLOCK)
        INNER JOIN ImportDetail IDT WITH (NOLOCK) ON IR.idReceipt = IDT.idReceipt
        LEFT JOIN Supplier SU WITH (NOLOCK) ON IR.idSupplier = SU.idSupplier
        LEFT JOIN Staff S WITH (NOLOCK) ON IR.importedBy = S.accountUserName
        WHERE IR.importDate BETWEEN @FromDate AND @ToDate
        GROUP BY IR.idReceipt, IR.importDate, SU.supplierName, S.fullName, IR.importedBy

        UNION ALL

        -- Dòng Tổng Thu
        SELECT 
            N'Tổng kết' AS [Loại],
            N'Tổng Thu' AS [Mã HĐ/PN],
            NULL AS [Ngày],
            NULL AS [Bàn/NCC],
            NULL AS [Tổng SL],
            FORMAT(@TongThu, 'N0') + N' VND' AS [Tổng Tiền],
            NULL AS [Nhân Viên]

        UNION ALL

        -- Dòng Tổng Chi
        SELECT 
            N'Tổng kết' AS [Loại],
            N'Tổng Chi' AS [Mã HĐ/PN],
            NULL AS [Ngày],
            NULL AS [Bàn/NCC],
            NULL AS [Tổng SL],
            FORMAT(@TongChi, 'N0') + N' VND' AS [Tổng Tiền],
            NULL AS [Nhân Viên]

        UNION ALL

        -- Dòng Lợi Nhuận
        SELECT 
            N'Tổng kết' AS [Loại],
            N'Lợi Nhuận' AS [Mã HĐ/PN],
            NULL AS [Ngày],
            NULL AS [Bàn/NCC],
            NULL AS [Tổng SL],
            FORMAT(@TongThu - @TongChi, 'N0') + N' VND' AS [Tổng Tiền],
            NULL AS [Nhân Viên]
    )
    SELECT 
        [Loại],
        [Mã HĐ/PN],
        [Ngày],
        [Bàn/NCC],
        [Tổng SL],
        [Tổng Tiền],
        [Nhân Viên]
    FROM CombinedResults
    ORDER BY 
        CASE 
            WHEN [Loại] = N'Thu' THEN 1 
            WHEN [Loại] = N'Chi' THEN 2 
            ELSE 3 
        END, 
        [Ngày] DESC, 
        [Mã HĐ/PN];
END
GO

exec GetImportDetails 2;


CREATE OR ALTER PROCEDURE SearchDoanhThuUniversal
    @Keyword NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    -- Normalize từ khóa
    SET @Keyword = LTRIM(RTRIM(@Keyword));

    ----------------------------------------------------
    -- Tìm trong Bill (Thu)
    ----------------------------------------------------
    SELECT 
        N'Thu' AS [Loại],
        CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
        MAX(B.DateCheckOut) AS [Ngày],
        MAX(T.tableName) AS [Bàn/NCC],
        SUM(BI.count) AS [Tổng SL],
        FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(MAX(T.tableName)) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(MAX(S.fullName), MAX(B.createdBy)) AS [Nhân Viên],
        1 AS SortOrder
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE B.status = 1
    GROUP BY B.idBill
    HAVING (
        @Keyword IS NULL OR
        CAST(B.idBill AS NVARCHAR) LIKE '%' + @Keyword + '%' OR
        MAX(T.tableName) LIKE '%' + @Keyword + '%' OR
        ISNULL(MAX(S.fullName), MAX(B.createdBy)) LIKE '%' + @Keyword + '%' OR
        CONVERT(NVARCHAR(20), MAX(B.DateCheckOut), 120) LIKE '%' + @Keyword + '%' OR
        CAST(SUM(BI.count * F.price) + CASE WHEN LOWER(MAX(T.tableName)) LIKE '%vip%' THEN 20000 ELSE 0 END AS NVARCHAR) LIKE '%' + @Keyword + '%'
    )

    UNION ALL

    ----------------------------------------------------
    -- Tìm trong ImportReceipt (Chi)
    ----------------------------------------------------
    SELECT 
        N'Chi' AS [Loại],
        CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
        MAX(IR.importDate) AS [Ngày],
        ISNULL(MAX(SU.supplierName), N'Không xác định') AS [Bàn/NCC],
        SUM(IDT.quantity) AS [Tổng SL],
        FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(MAX(S.fullName), MAX(IR.importedBy)) AS [Nhân Viên],
        2 AS SortOrder
    FROM ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    LEFT JOIN Supplier SU ON IR.idSupplier = SU.idSupplier
    LEFT JOIN Staff S ON IR.importedBy = S.accountUserName
    GROUP BY IR.idReceipt
    HAVING (
        @Keyword IS NULL OR
        CAST(IR.idReceipt AS NVARCHAR) LIKE '%' + @Keyword + '%' OR
        ISNULL(MAX(SU.supplierName), '') LIKE '%' + @Keyword + '%' OR
        ISNULL(MAX(S.fullName), MAX(IR.importedBy)) LIKE '%' + @Keyword + '%' OR
        CONVERT(NVARCHAR(20), MAX(IR.importDate), 120) LIKE '%' + @Keyword + '%' OR
        CAST(SUM(IDT.quantity * IDT.unitPrice) AS NVARCHAR) LIKE '%' + @Keyword + '%'
    )

    ORDER BY SortOrder, [Ngày] DESC, [Mã HĐ/PN];
END



CREATE OR ALTER PROCEDURE SearchDoanhThuMultiKeyword
    @Keywords KeywordTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    -- Phần thu (hóa đơn)
    SELECT 
        N'Thu' AS [Loại],
        CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
        MAX(B.DateCheckOut) AS [Ngày],
        MAX(T.tableName) AS [Bàn/NCC],
        SUM(BI.count) AS [Tổng SL],
        FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(MAX(T.tableName)) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(MAX(S.fullName), MAX(B.createdBy)) AS [Nhân Viên]
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    CROSS APPLY @Keywords K
    WHERE B.status = 1
        AND (
            CAST(B.idBill AS NVARCHAR) LIKE '%' + K.Keyword + '%'
            OR T.tableName LIKE '%' + K.Keyword + '%'
            OR ISNULL(S.fullName, B.createdBy) LIKE '%' + K.Keyword + '%'
            OR CONVERT(NVARCHAR(20), B.DateCheckOut, 120) LIKE '%' + K.Keyword + '%'
        )
    GROUP BY B.idBill

    UNION ALL

    -- Phần chi (phiếu nhập)
    SELECT 
        N'Chi' AS [Loại],
        CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
        MAX(IR.importDate) AS [Ngày],
        ISNULL(MAX(SU.supplierName), N'Không xác định') AS [Bàn/NCC],
        SUM(IDT.quantity) AS [Tổng SL],
        FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(MAX(S.fullName), MAX(IR.importedBy)) AS [Nhân Viên]
    FROM ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    LEFT JOIN Supplier SU ON IR.idSupplier = SU.idSupplier
    LEFT JOIN Staff S ON IR.importedBy = S.accountUserName
    CROSS APPLY @Keywords K
    WHERE 1 = 1
        AND (
            CAST(IR.idReceipt AS NVARCHAR) LIKE '%' + K.Keyword + '%'
            OR ISNULL(SU.supplierName, N'Không xác định') LIKE '%' + K.Keyword + '%'
            OR ISNULL(S.fullName, IR.importedBy) LIKE '%' + K.Keyword + '%'
            OR CONVERT(NVARCHAR(20), IR.importDate, 120) LIKE '%' + K.Keyword + '%'
        )
    GROUP BY IR.idReceipt

    ORDER BY 
        CASE 
            WHEN [Loại] = N'Thu' THEN 1 
            WHEN [Loại] = N'Chi' THEN 2 
        END, 
        [Ngày] DESC, 
        [Mã HĐ/PN];
END
GO



EXEC SearchDoanhThuMultiKeyword N'VIP 29/07 125000';
EXEC SearchDoanhThuByKeyword N'VIP';
EXEC SearchDoanhThuByKeyword N'07/29';
EXEC SearchDoanhThuByKeyword N'150000'; -- tìm theo tổng tiền

SearchDoanhThuByKeyword
CREATE OR ALTER PROCEDURE SearchDoanhThuByKeyword
    @Keyword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Thu (Bill)
    SELECT 
        N'Thu' AS [Loại],
        CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
        B.DateCheckOut AS [Ngày],
        T.tableName AS [Bàn/NCC],
        SUM(BI.count) AS [Tổng SL],
        FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, B.createdBy) AS [Nhân Viên],
        1 AS SortOrder
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE B.status = 1
      AND (
            CAST(B.idBill AS NVARCHAR) LIKE '%' + @Keyword + '%' OR
            T.tableName LIKE '%' + @Keyword + '%' OR
            ISNULL(S.fullName, B.createdBy) LIKE '%' + @Keyword + '%' OR
            FORMAT(B.DateCheckOut, 'yyyy-MM-dd HH:mm') LIKE '%' + @Keyword + '%'
          )
    GROUP BY B.idBill, B.DateCheckOut, T.tableName, S.fullName, B.createdBy

    UNION ALL

    -- Chi (ImportReceipt)
    SELECT 
        N'Chi' AS [Loại],
        CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
        IR.importDate AS [Ngày],
        ISNULL(SU.supplierName, N'Không xác định') AS [Bàn/NCC],
        SUM(IDT.quantity) AS [Tổng SL],
        FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, IR.importedBy) AS [Nhân Viên],
        2 AS SortOrder
    FROM ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    LEFT JOIN Supplier SU ON IR.idSupplier = SU.idSupplier
    LEFT JOIN Staff S ON IR.importedBy = S.accountUserName
    WHERE (
            CAST(IR.idReceipt AS NVARCHAR) LIKE '%' + @Keyword + '%' OR
            ISNULL(SU.supplierName, '') LIKE '%' + @Keyword + '%' OR
            ISNULL(S.fullName, IR.importedBy) LIKE '%' + @Keyword + '%' OR
            FORMAT(IR.importDate, 'yyyy-MM-dd HH:mm') LIKE '%' + @Keyword + '%'
          )
    GROUP BY IR.idReceipt, IR.importDate, SU.supplierName, S.fullName, IR.importedBy

    ORDER BY SortOrder, [Ngày] DESC, [Mã HĐ/PN];
END





CREATE OR ALTER PROCEDURE SearchDoanhThuByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Thu (Bill)
    SELECT 
        N'Thu' AS [Loại],
        CAST(B.idBill AS NVARCHAR) AS [Mã HĐ/PN],
        B.DateCheckOut AS [Ngày],
        T.tableName AS [Bàn/NCC],
        SUM(BI.count) AS [Tổng SL],
        FORMAT(SUM(BI.count * F.price) + CASE WHEN LOWER(T.tableName) LIKE '%vip%' THEN 20000 ELSE 0 END, 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, B.createdBy) AS [Nhân Viên],
        1 AS SortOrder
    FROM Bill B
    INNER JOIN TableFood T ON B.idTable = T.idTable
    INNER JOIN BillInfo BI ON B.idBill = BI.idBill
    INNER JOIN Food F ON BI.idFood = F.idFood
    LEFT JOIN Staff S ON B.createdBy = S.accountUserName
    WHERE B.status = 1 AND B.DateCheckOut BETWEEN @FromDate AND @ToDate
    GROUP BY B.idBill, B.DateCheckOut, T.tableName, S.fullName, B.createdBy

    UNION ALL

    -- Chi (ImportReceipt)
    SELECT 
        N'Chi' AS [Loại],
        CAST(IR.idReceipt AS NVARCHAR) AS [Mã HĐ/PN],
        IR.importDate AS [Ngày],
        ISNULL(SU.supplierName, N'Không xác định') AS [Bàn/NCC],
        SUM(IDT.quantity) AS [Tổng SL],
        FORMAT(SUM(IDT.quantity * IDT.unitPrice), 'N0') + N' VND' AS [Tổng Tiền],
        ISNULL(S.fullName, IR.importedBy) AS [Nhân Viên],
        2 AS SortOrder
    FROM ImportReceipt IR
    INNER JOIN ImportDetail IDT ON IR.idReceipt = IDT.idReceipt
    LEFT JOIN Supplier SU ON IR.idSupplier = SU.idSupplier
    LEFT JOIN Staff S ON IR.importedBy = S.accountUserName
    WHERE IR.importDate BETWEEN @FromDate AND @ToDate
    GROUP BY IR.idReceipt, IR.importDate, SU.supplierName, S.fullName, IR.importedBy

    ORDER BY SortOrder, [Ngày] DESC, [Mã HĐ/PN];
END
