var Category;
(function (Category) {
    Category[Category["Kitchen"] = 0] = "Kitchen";
    Category[Category["Math"] = 1] = "Math";
    Category[Category["Afroamerican_guy"] = 2] = "Afroamerican_guy";
})(Category || (Category = {}));
var Product = /** @class */ (function () {
    function Product(id, name, price, category, description) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.description = description !== null && description !== void 0 ? description : "Данил Колбасенко";
    }
    Product.prototype.getInfo = function () {
        return "id: ".concat(this.id, ", name: ").concat(this.name, ", price: ").concat(this.price, ", description: ").concat(this.description, ", category: ").concat(this.category);
    };
    return Product;
}());
var Catalog = /** @class */ (function () {
    function Catalog() {
        this.products = [];
        console.log(Array.isArray(this.products));
    }
    Catalog.prototype.addProduct = function (data) {
        var _a;
        var newId = Math.floor(Math.random() * 500);
        var newProduct = new Product(newId, data.name, data.price, data.category, (_a = data.description) !== null && _a !== void 0 ? _a : "Здесь могла быть ваша реклама");
        this.products.push(newProduct);
        return newProduct;
    };
    Catalog.prototype.removeProduct = function (id) {
        this.products = this.products.filter(function (p) { return p.id != id; });
    };
    Catalog.prototype.getProductById = function (id) {
        return this.products.find(function (p) { return p.id === id; });
    };
    Catalog.prototype.getAllProducts = function () {
        return this.products;
    };
    Catalog.prototype.getproductByCategory = function (category) {
        return this.products.find(function (p) { return p.category === category; });
    };
    Catalog.prototype.updateProductById = function (id, update) {
        var product = this.getProductById(id);
        if (typeof (product) == 'object') {
            Object.assign(product, update);
            console.log("\u0422\u043E\u0432\u0430\u0440 ".concat(id, " \u043E\u0431\u043D\u043E\u0432\u043B\u0435\u043D"));
        }
    };
    return Catalog;
}());
var Order = /** @class */ (function () {
    function Order(id, customerId) {
        this.id = id;
        this.products = [];
        this.totalPrice = 0;
        this.customerId = customerId;
    }
    Order.prototype.calculateTotalPrice = function () {
        var _this = this;
        this.totalPrice = 0;
        this.products.forEach(function (element) {
            _this.totalPrice += element.price;
        });
    };
    Order.prototype.getOrderByInfo = function () {
        return "ID: ".concat(this.id, ",\n \u0442\u043E\u0432\u0430\u0440\u044B \u0437\u0430\u043A\u0430\u0437\u0430: ").concat(this.products, ",\n \u043F\u043E\u043B\u043D\u0430\u044F \u0446\u0435\u043D\u0430 \u0442\u043E\u0432\u0430\u0440\u0430: ").concat(this.totalPrice);
    };
    return Order;
}());
var pattern = "^[a-zA-Z0-9]{4,50}@gmail\.com$";
var regex = new RegExp(pattern);
var Customer = /** @class */ (function () {
    function Customer(id, name, email) {
        this.id = id;
        this.name = name;
        if (regex.test(email)) {
            this.email = email;
        }
        else {
            this.email = "nuhaibebru228@gmail.com";
        }
    }
    Customer.prototype.getCustomerInfo = function () {
        return "ID: ".concat(this.id, ", name: ").concat(this.name, ", email: ").concat(this.email);
    };
    return Customer;
}());
var OrderManager = /** @class */ (function () {
    function OrderManager(orders) {
        this.orders = orders !== null && orders !== void 0 ? orders : [];
    }
    ;
    OrderManager.prototype.createOrder = function (customer, products) {
        var id = Math.floor(Math.random() * 500);
        var order = new Order(id, customer.id);
        order.products = products;
        order.calculateTotalPrice();
        this.orders.push(order);
        return order;
    };
    OrderManager.prototype.getorderById = function (id) {
        return this.orders.find(function (p) { return p.id === id; });
    };
    OrderManager.prototype.getAllOrders = function () {
        return this.orders;
    };
    OrderManager.prototype.getOrdersByCustomer = function (customerId) {
        return this.orders.find(function (p) { return p.customerId === customerId; });
    };
    return OrderManager;
}());
var t = new Catalog();
