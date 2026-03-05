//1
interface Order {
  item: string;
  price: number;
}

function checkStock(item: string): Promise<Order> {
  return new Promise<Order>((resolve, reject) => {
    console.log("Проверяем наличие товара...");

    setTimeout(() => {
      const stock: string[] = ["bim", "bom", "bam"];

      if (stock.includes(item)) {
        resolve({ item, price: 700 });
      } else {
        reject(new Error("Товара нет в наличии"));
      }
    }, 1000);
  });
}

function processPayment(order: Order): Promise<string> {
  return new Promise<string>((resolve, reject) => {
    console.log("Обработка оплаты...");

    setTimeout(() => {
      const userBalance: number = 1000;

      if (userBalance >= 500) {
        resolve("Красавчик, оплатил");
      } else {
        reject(new Error("Недостаточно средств"));
      }
    }, 2000);
  });
}

function deliverOrder(): Promise<string> {
  return new Promise<string>((resolve) => {
    console.log("Доставка заказа...");

    setTimeout(() => {
      resolve("Заказ на месте!");
    }, 1500);
  });
}

checkStock("afroamerican")
  .then((order: Order) => processPayment(order))
  .then((result: string) => {
    console.log(result);
    return deliverOrder();
  })
  .then((delivery: string) => console.log(delivery))
  .catch((error: Error) => console.error("Ошибка:", error.message))
  .finally((): void => {
    console.log("Спасибо за заказ, приходите еще!");
  });


//2
function fetchFast(): Promise<string> {
  return new Promise<string>((resolve) => {
    setTimeout(() => {
      resolve("Молния маквин, кчау");
    }, 500);
  });
}

function fetchSlow(): Promise<string> {
  return new Promise<string>((resolve) => {
    setTimeout(() => {
      resolve("Гром всегда позже молнии");
    }, 2000);
  });
}

Promise.race<string>([fetchFast(), fetchSlow()])
  .then((result: string) => console.log("Победитель:", result))
  .catch((error: unknown) => console.error(error));


//3
const promises: Promise<string>[] = [
  Promise.resolve("Успех 1"),
  Promise.reject(new Error("Ошибка 1")),
  Promise.resolve("Успех 2"),
  Promise.reject(new Error("Ошибка 2")),
  Promise.resolve("Успех 3")
];

Promise.allSettled(promises)
  .then((results: PromiseSettledResult<string>[]) => {

    const successful: string[] = results
      .filter(
        (r): r is PromiseFulfilledResult<string> =>
          r.status === "fulfilled"
      )
      .map(r => r.value);

    console.log("Красавчики:", successful);
  });


//4
console.log("Начало");

setTimeout((): void => console.log("Таймаут"), 0);

Promise.resolve<void>(undefined)
  .then((): void => console.log("Промис 1"))
  .then((): void => console.log("Промис 2"));

console.log("Конец");


//5
async function getData(): Promise<void> {
  try {
    const response: Response = await fetch("https://api.example.com/data");

    if (!response.ok) {
      throw new Error("Ошибка HTTP: " + response.status);
    }

    const data: unknown = await response.json();
    console.log(data);

  } catch (err: unknown) {
    if (err instanceof Error) {
      console.error("Ошибка:", err.message);
    } else {
      console.error("Неизвестная ошибка");
    }
  }
}


//6
async function limitRequests<T>(
  tasks: Array<() => Promise<T>>,
  limit: number
): Promise<T[]> {

  const results: T[] = [];
  let index: number = 0;

  async function worker(): Promise<void> {
    while (index < tasks.length) {
      const currentIndex: number = index++;

      try {
        results[currentIndex] = await tasks[currentIndex]();
      } catch (err) {
        throw err;
      }
    }
  }

  const workers: Promise<void>[] = [];

  for (let i = 0; i < limit; i++) {
    workers.push(worker());
  }

  await Promise.all(workers);

  return results;
}


function createTask(id: number): () => Promise<string> {
  return (): Promise<string> =>
    new Promise<string>((resolve) => {
      console.log("Запуск задачи", id);

      setTimeout(() => {
        console.log("Завершена задача", id);
        resolve("Результат " + id);
      }, 1000);
    });
}

const tasks: Array<() => Promise<string>> =
  Array.from({ length: 10 }, (_, i) => createTask(i + 1));

limitRequests<string>(tasks, 3)
  .then((results: string[]) => {
    console.log("Все задачи завершены:");
    console.log(results);
  })
  .catch((error: unknown) => console.error(error));