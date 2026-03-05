enum Category{
    Kitchen,
    Math,
    Afroamerican_guy
}

class Product{
    public id: number;
    public name:string;
    public price:number;
    public description?:string;
    public category:Category;

    constructor(id:number, name:string, price:number, category:Category, description?:string)
    {
        this.id=id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.description = description ?? "Данил Колбасенко";
    }

    getInfo()
    {
        return `id: ${this.id}, name: ${this.name}, price: ${this.price}, description: ${this.description}, category: ${this.category}`;
    }

}

type CreateProduct = Omit<Product, 'id'>;

class Catalog
{
    products: Product[];
    constructor()
    {
        this.products = [];
        console.log(Array.isArray(this.products));
    }

    addProduct(data: CreateProduct):Product
    {
        let newId: number = Math.floor(Math.random()*500);
        const newProduct = new Product(
            newId,
            data.name,
            data.price,
            data.category,
            data.description ?? "Здесь могла быть ваша реклама"
        );
        this.products.push(newProduct);
        return newProduct;
    }

    removeProduct(id:number)
    {
        this.products = this.products.filter(p=>p.id!=id);
    }

    getProductById(id: number):Product|undefined
    {
        return this.products.find(p=>p.id===id);
    }

    getAllProducts():Product[]
    {
        return this.products;
    }

    getproductByCategory(category: Category) : Product | undefined
    {
        return this.products.find(p=>p.category===category);
       
    }

    updateProductById(id: number, update: Partial<Product>)
    {
        const product = this.getProductById(id);
        if(typeof(product)=='object')
        {
            Object.assign(product, update);
            console.log(`Товар ${id} обновлен`);
        }
    }
}


class Order<T extends Product>
{
    id:number;
    products:Array<T>;
    totalPrice:number;
    customerId: number;

    constructor(id:number, customerId: number)
    {
        this.id = id;
        this.products=[];
        this.totalPrice = 0;
        this.customerId = customerId;
    }

    calculateTotalPrice()
    {
        this.totalPrice = 0;
        this.products.forEach(element => {
            this.totalPrice+=element.price;
        });
    }

    getOrderByInfo():string
    {
        return `ID: ${this.id},\n товары заказа: ${this.products},\n полная цена товара: ${this.totalPrice}`;
    }
}

let pattern: string = "^[a-zA-Z0-9]{4,50}@gmail\.com$";
const regex:RegExp = new RegExp(pattern);

class Customer{
    id: number;
    name: string;
    email: string;

    constructor(id:number, name:string, email:string)
    {
        this.id = id;
        this.name = name;
        if(regex.test(email))
        {
            this.email = email;
        }else{
            this.email = "nuhaibebru228@gmail.com";
        }
    }

    getCustomerInfo():string
    {
        return `ID: ${this.id}, name: ${this.name}, email: ${this.email}`;
    }
}

type InfoOrder = Pick<Order<Product>, 'id' | 'totalPrice'>;

class OrderManager
{
    orders: Array<Order<Product>>;
    constructor();

    constructor(orders?: Array<Order<Product>>)
    {
        this.orders = orders ?? [];
    };

    createOrder(customer: Customer, products: Product[]): Order<Product>
    {   
        let id:number = Math.floor(Math.random()*500);
        let order: Order<Product> = new Order<Product>(id, customer.id);
        order.products = products;
        order.calculateTotalPrice();
        this.orders.push(order);
        return order;
    }


    getorderById(id: number): InfoOrder | undefined
    {
        return this.orders.find(p=>p.id===id);
    }

    getAllOrders():InfoOrder[]
    {
        return this.orders;
    }

    getOrdersByCustomer(customerId: number): InfoOrder | undefined
    {
        return this.orders.find(p=>p.customerId===customerId);
    }
}

let t:Catalog = new Catalog();