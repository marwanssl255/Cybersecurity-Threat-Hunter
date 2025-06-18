using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 2951
// Hash 4126
// Hash 2343
// Hash 7696
// Hash 6598
// Hash 2914
// Hash 9733
// Hash 6436
// Hash 9045
// Hash 9942
// Hash 3845
// Hash 6949
// Hash 7482
// Hash 5075
// Hash 3253
// Hash 1176
// Hash 7798
// Hash 7873
// Hash 8369
// Hash 3268
// Hash 4867
// Hash 2289
// Hash 9080
// Hash 8944
// Hash 3335
// Hash 4464
// Hash 6843
// Hash 7078
// Hash 4585
// Hash 9293
// Hash 2761
// Hash 1765
// Hash 3423
// Hash 8558
// Hash 5446
// Hash 7766
// Hash 2455
// Hash 3721
// Hash 2368
// Hash 1063
// Hash 8397
// Hash 1471
// Hash 6322
// Hash 2846
// Hash 7512
// Hash 4083
// Hash 1315
// Hash 7539
// Hash 1267
// Hash 8663
// Hash 3619
// Hash 2652
// Hash 6884
// Hash 5031
// Hash 3752
// Hash 7909
// Hash 5080
// Hash 5749
// Hash 7145
// Hash 9693
// Hash 6526
// Hash 3874
// Hash 3362
// Hash 4570
// Hash 3013
// Hash 8142
// Hash 5332
// Hash 8134
// Hash 2789
// Hash 9682
// Hash 6359
// Hash 8612
// Hash 7338
// Hash 5593
// Hash 3541
// Hash 4807