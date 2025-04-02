import BookCard, { type BookCardProps } from '@/components/BookCard'
import { createFileRoute } from '@tanstack/react-router'

async function fetchData (): Promise<BookCardProps[]> {
  const bookResonse = await(await fetch('http://localhost:5296/api/books')).json()
  console.log(bookResonse)
  return bookResonse
}

export const Route = createFileRoute('/')({
  component: App,
  loader: fetchData
})

function App() {
  const data = Route.useLoaderData()
  return (
    <div className="min-h-10/12 bg-gray-100 flex flex-col">
      <main className="flex-grow px-6 py-8">
        <h1 className="text-3xl font-semibold text-gray-900 mb-6">Dashboard</h1>

        <div className="bg-white rounded-lg shadow p-3 border border-dashed border-gray-300 min-h-[300px]">
          <div className="h-full w-full bg-[repeating-linear-gradient(45deg, #f3f4f6, #f3f4f6 10px, #e5e7eb 10px, #e5e7eb 20px)] border border-slate-200 rounded-md" >
          <div className="flex md:flex-row flex-col flex-wrap space-x-32 md:gap-7 gap-5 p-2 justify-center w-full">
          {data.map(book => (
              <BookCard
                key={book.bookId}
                bookBlob={book.bookBlob}
                bookId={book.bookId}
                publishedDate={book.publishedDate}
                pageCount={book.pageCount}
                publishingHouse={book.publishingHouse}
                title={book.title}
                authorName={book.authorName}
                bookImageUrl={book.bookImageUrl}
              />
            ))}
          </div>
          </div>
          
        </div>
      </main>
    </div>
  )
}
