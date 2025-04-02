import { Link } from "@tanstack/react-router"

export interface BookCardProps {
  title: string
  bookImageUrl: string
  bookBlob: string
  authorName: string
  bookId: string
  pageCount: number
  publishedDate: string
  publishingHouse: string
}


export default function BookCard(props: BookCardProps) {
    const { title, bookBlob, bookImageUrl, pageCount, publishedDate, publishingHouse } = props
    return (
        <Link
            to={`/books/${props.bookId}`}
            className="flex rounded-md shadow-md md:shadow-xl overflow-hidden bg-white w-full md:w-4/12 transition delay-150 duration-300 ease-in-out hover:-translate-y-1.5 hover:scale-110 hover:cursor-pointer"
        >
            <div className="w-2/3">
                <img src={bookImageUrl ?? 'https://picsum.photos/300'} alt="Book cover" className="h-full w-full object-cover" />
            </div>
            <div className="w-10/12 pb-3">
                <div className="bg-red-900 min-w-full h-1/4 text-white text-center items-center flex justify-center">
                <h2 className="text-normal text-white font-bold mb-2">{title}</h2>
                </div>
                <div className="p-2">
                    <p className="text-gray-700 mb-1">
                        <span className="font-semibold mr-2">Author :</span><span>{props.authorName}</span>
                    </p>
                    <p className="text-gray-700 mb-1">
                        <span className="font-semibold mr-2">Pages :</span><span>{pageCount}</span>
                    </p>
                    <p className="text-gray-700 mt-2">
                        <span className="font-semibold mr-2">Publisher :</span><span>{publishingHouse}</span>
                    </p>
                </div>
            </div>
        </Link>

    )
}