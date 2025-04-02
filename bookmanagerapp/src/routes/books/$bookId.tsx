import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/books/$bookId')({
  component: RouteComponent,
  loader: async ({ params }) => {
    const bookId = params.bookId
    const bookResonse = await (await fetch(`http://localhost:5296/api/books/${bookId}`)).json()
    return bookResonse
  }
})

function RouteComponent() {
  const data = Route.useLoaderData();
  console.log(data);
  return (
    <div className="min-h-10/12 bg-gray-100 flex flex-col">
      <main className="flex-grow px-6 py-8">
        <h1 className="text-3xl font-semibold text-gray-900 mb-6">
          {data.bookTitle}
        </h1>
        <div className="bg-white rounded-lg shadow p-3 border border-dashed border-gray-300 min-h-[300px]">
          <div className="h-full w-full bg-[repeating-linear-gradient(45deg, #f3f4f6, #f3f4f6 10px, #e5e7eb 10px, #e5e7eb 20px)] border border-gray-200 rounded-md">
            <div className="flex md:flex-row flex-col flex-wrap space-x-32 md:gap-7 gap-5 p-2 justify-center w-full">
              <div className="max-w-lg bg-white border border-stone-200 rounded-lg shadow-sm dark:bg-stone-200 dark:border-gray-700">
                <a href="#">
                  <img
                    className="rounded-t-lg"
                    src="https://picsum.photos/520/300"
                    alt=""
                  />
                </a>
                <div className="p-5">
                  <div className="flex flex-row justify-between items-center">
                  <a href="#">
                    <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 ">
                      {data.authorName.split(',').map((word: string) => { 
                          let names = ''
                          names += word
                          return names
                        }).join(' ')
                      }
                    </h5>
                  </a>
                  <span className="font-bold space-x-3">
                    <span>Published :</span><span>{new Date(data.publishDate).getUTCFullYear()}</span>
                  </span>
                  </div>
                  <p className="mb-3 font-normal text-gray-700 dark:text-gray-700">
                    Here are the biggest enterprise technology acquisitions of
                    2021 so far, in reverse chronological order.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
