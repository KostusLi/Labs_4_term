enum Category{
    Kitchen,
    Math,
    Nigger
}

class Product{
    public id:bigint;
    public name:string;
    public price:number;
    public description?:string;
    public category:Category;

    constructor(id:bigint, name:string, price:number, description:string, category:Category):void
    {
        this.id=id;
        this.name = name;
        this.price = price;
        this.description = description;
        this.category = category;
    }

    getInfo()
    {
        return `id: ${this.id}, name: ${this.name}, price: ${this.price}, description: ${this.description}, category: ${this.category}`;
    }
}